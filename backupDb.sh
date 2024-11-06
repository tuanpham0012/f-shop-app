#--------Cơ chế backup DATABASE ----------------

CONTAINER_NAME=$s1
DATABASE_NAME=$s2
DATABASE_USERNAME=$s3
DATABASE_PASSWORD=$s4
HOSTFILEPATH=$s1 #/backups/sql-server/

echo "Tạo tên file backup với thời gian hiện tại"
DateTime=$(date +'%Y_%m_%d_%H_%M_%S')
FileName="${DATABASE_NAME}_${DateTime}"

echo ""
echo "Tạo thư mục lưu trữ file backup trên server"
mkdir -p $HOSTFILEPATH

echo ""
echo "Bắt đầu backup database '$DATABASE_NAME' trên docker_container '$CONTAINER_NAME' với tên file '$FileName'"
docker exec -it $CONTAINER_NAME /opt/mssql-tools18/bin/sqlcmd -S localhost -U $DATABASE_USERNAME -P $DATABASE_PASSWORD -C -Q "BACKUP DATABASE [$DATABASE_NAME] TO DISK = N'/var/opt/mssql/backup/$FileName.bak' WITH COMPRESSION, NOINIT, NAME = '$DATABASE_NAME-full', SKIP, NOREWIND, NOUNLOAD, STATS = 5"

echo ""
echo "Sao chep file backup từ docker_container về thư mục lưu trữ file backup trên server"
docker cp $CONTAINER_NAME':'/var/opt/mssql/backup/$FileName.bak $HOSTFILEPATH/$FileName.bak

echo ""
echo "Xoá file backup trên docker_container sau khi backup xong"
docker exec -i $CONTAINER_NAME rm -rf /var/opt/mssql/backup/$FileName.bak

echo ""
echo "Đã sao lưu database '$DATABASE_NAME' vào $HOSTFILEPATH/$FileName.bak"
echo "Hoàn thành!"
