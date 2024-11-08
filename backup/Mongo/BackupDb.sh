CONTAINER_NAME=$1
DATABASE_NAME=$2
MONGO_INITDB_ROOT_USERNAME=$3
MONGO_INITDB_ROOT_PW=$4
HOST_FILE_PATH=$5
MONGO_VOLUME_PATH=$6 -n ? $6 : "/data"



echo "Tạo tên file backup với thời gian hiện tại"
$dateTime = Get-Date -Format yyyy_MM_dd #(Get-Date).tostring("yyyy_MM_dd")
$FILE_NAME=$DATABASE_NAME + "_" + $dateTime

echo ""
echo "Tạo thư mục lưu trữ file backup trên server"
mkdir -p $HOST_FILE_PATH


echo ""
echo "Bắt đầu backup database '$DATABASE_NAME' trên docker_container '$CONTAINER_NAME' với tên file '$FILE_NAME'"

docker exec -i $CONTAINER_NAME /usr/bin/mongodump --username $MONGO_INITDB_ROOT_USERNAME --host=localhost --password $MONGO_INITDB_ROOT_PW --authenticationDatabase admin --db $DATABASE_NAME --archive=$MONGO_VOLUME_PATH/$FILE_NAME.dump

echo ""
echo "Sao chep file backup từ docker_container về thư mục lưu trữ file backup trên server"
docker cp $CONTAINER_NAME':'$MONGO_VOLUME_PATH/$FILE_NAME.dump $HOST_FILE_PATH\$FILE_NAME.dump

echo ""
echo "Xoá file backup trên docker_container sau khi backup xong"
docker exec -i $CONTAINER_NAME rm -rf $MONGO_VOLUME_PATH/$FILE_NAME.dump

echo ""
echo "Đã sao lưu database '$DATABASE_NAME' vào $HOST_FILE_PATH/$FILE_NAME.dump"
echo "Hoàn thành!"