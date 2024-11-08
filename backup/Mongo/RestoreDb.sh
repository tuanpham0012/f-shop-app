
CONTAINER_NAME=$1
DATABASE_NAME=$2
MONGO_INITDB_ROOT_USERNAME=$3
MONGO_INITDB_ROOT_PW=$4
RESTORE_FILE_PATH=$5
RESTORE_FILE_NAME=$6
MONGO_VOLUME_PATH=$7 -n ? $7 : "/data"
echo ""
echo "Tạo thư mục lưu trữ file backup trên docker_container. Chú ý: container phải đã được chạy!"
docker exec $CONTAINER_NAME mkdir -p $MONGO_VOLUME_PATH/restores

echo ""
echo "Sao chép file backup từ $RESTORE_FILE_NAME đến docker_container '$CONTAINER_NAME'. Chú ý: container phải đã được chạy!"
$CopyFilePath="${RESTORE_FILE_PATH}/${RESTORE_FILE_NAME}"
docker cp $CopyFilePath  $CONTAINER_NAME':'$MONGO_VOLUME_PATH/restores/$RESTORE_FILE_NAME

echo ""
echo "Bắt đầu restore database '$DATABASE_NAME' trên docker_container '$CONTAINER_NAME'"
docker exec -i $CONTAINER_NAME /usr/bin/mongorestore --username $MONGO_INITDB_ROOT_USERNAME --password $MONGO_INITDB_ROOT_PW --authenticationDatabase admin --nsInclude="$DATABASE_NAME.*" --drop --archive=$MONGO_VOLUME_PATH/restores/$RESTORE_FILE_NAME

echo ""
echo "Xoá file backup trên docker_container sau khi restore xong"
docker exec -i $CONTAINER_NAME rm -rf $MONGO_VOLUME_PATH/restores/$RESTORE_FILE_NAME

echo ""
echo "Đã khôi phục database '$DATABASE_NAME' trên docker_container '$CONTAINER_NAME' từ file $RESTORE_FILE_NAME"
echo "Hoàn thành!"