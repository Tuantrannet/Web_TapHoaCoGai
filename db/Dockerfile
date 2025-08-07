FROM mcr.microsoft.com/mssql/server:2022-latest

ENV DEBIAN_FRONTEND=noninteractive
ENV TZ=Asia/Ho_Chi_Minh

USER root

# Bước 1: Cập nhật và cài tzdata trước
RUN apt-get update && \
    apt-get install -y tzdata && \
    ln -fs /usr/share/zoneinfo/$TZ /etc/localtime && \
    dpkg-reconfigure --frontend noninteractive tzdata

# Bước 2: Cài curl và gnupg
RUN apt-get update && \
    apt-get install -y curl gnupg

# Bước 3: Thêm Microsoft repo cho Ubuntu 22.04
RUN curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor -o /usr/share/keyrings/microsoft.gpg && \
    echo "deb [arch=amd64 signed-by=/usr/share/keyrings/microsoft.gpg] https://packages.microsoft.com/ubuntu/22.04/prod jammy main" \
    > /etc/apt/sources.list.d/mssql-release.list

# Bước 4: Cài mssql-tools
RUN apt-get update && \
    ACCEPT_EULA=Y apt-get install -y mssql-tools unixodbc-dev

# Bước 5: Tạo alias cho sqlcmd
RUN ln -sfn /opt/mssql-tools/bin/sqlcmd /usr/bin/sqlcmd && \
    ln -sfn /opt/mssql-tools/bin/bcp /usr/bin/bcp

# Cleanup
RUN apt-get clean && rm -rf /var/lib/apt/lists/*
