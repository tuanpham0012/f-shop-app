
#------------Cơ chế Restore DATABASE ------------
CONTAINER_NAME=$1
DATABASE_NAME=$2
DATABASE_USERNAME=$3
DATABASE_PASSWORD=$4
RESTOREFILEPATH=$5
RESTOREFILENAME=$6

MSSQL_TOOL="mssql-tools"

echo "Kiểm tra mssql-tools"
docker exec -i $CONTAINER_NAME sh -c "test -f /opt/mssql-tools18/bin/sqlcmd"
file_exists=$?
if [ $file_exists -eq 0 ]; then
    echo "MSSQL_TOOL : mssql-tools18"
    MSSQL_TOOL="mssql-tools18"
fi

echo ""
echo "Tạo thư mục lưu trữ file backup trên docker_container. Chú ý: container phải đã được chạy!"
docker exec $CONTAINER_NAME mkdir -p /var/opt/mssql/restores

echo ""
echo "Sao chép file backup từ $RESTOREFILENAME đến docker_container '$CONTAINER_NAME'. Chú ý: container phải đã được chạy!"

CopyFilePath="${RESTOREFILEPATH}/${RESTOREFILENAME}"

echo $CopyFilePath

docker cp $CopyFilePath $CONTAINER_NAME':'/var/opt/mssql/restores/$RESTOREFILENAME

echo ""
echo "Kiểm tra '$DATABASE_NAME' có tồn tại trong CSDL hay không. Nếu tồn tại thì cần SET OFFLINE cho '$DATABASE_NAME' rồi mới Restore và cần SET ONLINE lại. Nếu Không thì Restore luôn!"

CheckExists=$(docker exec -i $CONTAINER_NAME /opt/$MSSQL_TOOL/bin/sqlcmd -S localhost -U $DATABASE_USERNAME -P $DATABASE_PASSWORD -C -Q "IF DB_ID('$DATABASE_NAME') IS NOT NULL PRINT '1' ELSE PRINT '0'")
echo ""
echo "EXISTS DATABASE = '$CheckExists'"
if [ $CheckExists -eq "1" ]
then
    echo ""
    echo "Cài đặt trạng thái Offline cho database '$DATABASE_NAME' trên docker_container '$CONTAINER_NAME'"
    docker exec -i $CONTAINER_NAME /opt/$MSSQL_TOOL/bin/sqlcmd -S localhost -U $DATABASE_USERNAME -P $DATABASE_PASSWORD -C -Q "ALTER DATABASE $DATABASE_NAME SET OFFLINE WITH ROLLBACK IMMEDIATE"

    echo ""
    echo "Bắt đầu restore database '$DATABASE_NAME' trên docker_container '$CONTAINER_NAME'"
    docker exec -i $CONTAINER_NAME /opt/$MSSQL_TOOL/bin/sqlcmd -S localhost -U $DATABASE_USERNAME -P $DATABASE_PASSWORD -C -Q "RESTORE DATABASE [$DATABASE_NAME] FROM DISK = N'/var/opt/mssql/restores/$RESTOREFILENAME' WITH FILE = 1, NOUNLOAD, REPLACE, RECOVERY, STATS = 5"

    echo ""
    echo "Cài đặt trạng thái Online cho database '$DATABASE_NAME' trên docker_container '$CONTAINER_NAME'"
    docker exec -i $CONTAINER_NAME /opt/$MSSQL_TOOL/bin/sqlcmd -S localhost -U $DATABASE_USERNAME -P $DATABASE_PASSWORD -C -Q "ALTER DATABASE $DATABASE_NAME SET ONLINE WITH ROLLBACK IMMEDIATE"
else
    echo ""
    echo "Bắt đầu restore database '$DATABASE_NAME' trên docker_container '$CONTAINER_NAME'"
    docker exec -i $CONTAINER_NAME /opt/$MSSQL_TOOL/bin/sqlcmd -S localhost -U $DATABASE_USERNAME -P $DATABASE_PASSWORD -C -Q "RESTORE DATABASE [$DATABASE_NAME] FROM DISK = N'/var/opt/mssql/restores/$RESTOREFILENAME' WITH FILE = 1, NOUNLOAD, REPLACE, RECOVERY, STATS = 5"
fi

echo ""
echo "Xoá file backup trên docker_container sau khi restore xong"
docker exec -i $CONTAINER_NAME rm -rf /var/opt/mssql/restores/$RESTOREFILENAME

echo ""
echo "Đã khôi phục database '$DATABASE_NAME' trên docker_container '$CONTAINER_NAME' từ file $RESTOREFILENAME"
echo "Hoàn thành!"