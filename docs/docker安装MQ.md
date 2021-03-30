## 配置docker镜像

1. docker-machine ssh default

2. sudo vi /var/lib/boot2docker/profile

3. 在-label provider后添加

   --registry-mirror https://3otdxeyn.mirror.aliyuncs.com

4. exit

5. docker-machine restart default

6. docker info 查看

## docker安装RabbitMQ

1. 搜索镜像：**docker search rabbitmq**
2. 拉取镜像：**docker full rabbitmq**

3. 查看是否拉取成功：**docker images**

4. 根据镜像创建启动容器：

   ```
   docker run 
   -d 
   --name myRabbitmq
   -p 5672:5672
   -p 15672:15672 
   -v pwd/data:/usr/local/rabbitmq-docker 
   --hostname myRabbit
   -e RABBITMQ_DEFAULT_VHOST=my_vhost 
   -e RABBITMQ_DEFAULT_USER=admin 
   -e RABBITMQ_DEFAULT_PASS=admin 
   7471fb821b97
   
   docker run -d --name myRabbitmq -p 5672:5672 -p 15672:15672 --hostname myRabbit -e RABBITMQ_DEFAULT_VHOST=my_vhost -e RABBITMQ_DEFAULT_USER=admin -e RABBITMQ_DEFAULT_PASS=admin 7471fb821b97
   
   
   -d：后台运行容器
   --name：指定容器运行的名字
   -p：指定服务运行的端口(5672：应用访问端口；15672：控制台Web端口号)
   -v：映射的目录
   --hostname：主机名(RabbitMQ的一个重要注意事项是它根据所谓的 “节点名称” 存储数据，默认为主机名)
   -e：指定环境变量(
   RABBITMQ_DEFAULT_VHOST：默认虚拟机名；
   RABBITMQ_DEFAULT_USER：默认的用户名；
   RABBITMQ_DEFAULT_PASS：默认用户名的密码)
   7471fb821b97：下载的镜像id
   ```

   

5. 查看正在运行的容器：**docker ps**

6. 进入web管理页面：**ip:15672**

   **此时会访问不到页面，因为默认的web界面管理插件是关闭的需要手动开启**

   1. 进入容器内部：**docker exec -it 容器ID /bin/bash**
   2. 执行命令：**rabbitmq-plugins enable rabbitmq_management**
   3. 在浏览器中访问页面