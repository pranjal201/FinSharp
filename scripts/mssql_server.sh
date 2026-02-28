#!/bin/bash
#!/bin/bash

CONTAINER_NAME="mssqlserver"
IMAGE_NAME="mcr.microsoft.com/mssql/server:2022-latest"
SA_PASSWORD="YourStrong@Pass123"
PORT="1433"

echo "🔍 Checking if Docker is installed..."

if ! command -v docker &> /dev/null
then
    echo "❌ Docker is not installed. Please install Docker Desktop."
    exit 1
fi

echo "✅ Docker is installed."

echo "🔍 Checking if SQL Server image exists..."

if [[ "$(docker images -q $IMAGE_NAME 2> /dev/null)" == "" ]]; then
    echo "⬇️  Image not found. Pulling SQL Server 2022 image..."
    docker pull $IMAGE_NAME
else
    echo "✅ SQL Server image already exists."
fi

echo "🔍 Checking if container already exists..."

if [ "$(docker ps -aq -f name=$CONTAINER_NAME)" ]; then
    echo "⚠️ Container already exists. Removing old container..."
    docker rm -f $CONTAINER_NAME
fi

echo "🚀 Starting SQL Server container..."

docker run -e "ACCEPT_EULA=Y" \
-e "MSSQL_SA_PASSWORD=$SA_PASSWORD" \
-p $PORT:1433 \
--name $CONTAINER_NAME \
--platform linux/amd64 \
-v sqlvolume:/var/opt/mssql \
-d $IMAGE_NAME

echo "⏳ Waiting for SQL Server to initialize..."
sleep 10

echo "📦 Container Details:"
docker ps -f name=$CONTAINER_NAME

echo ""
echo "🔗 Connection Info:"
echo "Server: localhost,$PORT"
echo "Username: sa"
echo "Password: $SA_PASSWORD"

echo ""
echo "📜 To view live logs:"
echo "docker logs -f $CONTAINER_NAME"

echo ""
echo "🛑 To stop container:"
echo "docker stop $CONTAINER_NAME"

echo ""
echo "🗑 To remove container:"
echo "docker rm -f $CONTAINER_NAME"