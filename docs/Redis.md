# NoSQL

1. 为什么要用NoSQL数据库

   - 解决CPU及内存压力

     比如在集群分布式系统中，用户数据存在哪里？

     1. 存储在客户端cookie中：但是不安全
     2. session复制：数据冗余
     3. session存储在内存(NoSQL)里面

   - 解决IO压力：缓存数据库

2. nosql(非关系型数据库)基本概述

   Nosql不依赖业务逻辑方式存储，以简单的**key-value**模式存储。因此增加了数据库的扩展能力。

   特点：

   - 不遵循SQL标准
   - 不支持ACID
   - 远超于SQL的性能

3. NoSql的适用场景和不适用场景

   - 适用场景：
     1. 对数据高并发的读写
     2. 海量数据的读写
     3. 对数据高可扩展性
   - 不适用场景
     1. 需要事务支持
     2. 基于SQL的结构化查询存储，处理复杂的关系

4. 常见的NoSql数据库

   - Memcache

     数据存在内存里，不支持持久化

     支持简单的key-value模式，数据类型单一：字符串

     一般作为缓存数据库辅助持久化的数据库

   - Redis

     数据存在内存里，支持持久化(主要用于备份恢复)

     支持简单的key-value模式，还支持多种数据结构的存储：list, set, hash, zset(有序集合)等

     一般作为缓存数据库辅助持久化的数据库

   - MongoDB

     高性能，开源，模式自由的文档型数据库

     数据存在内存里面，当内存不够时，把不常用的数据保存在硬盘里面

     虽然key-value模式，但是对value(如：json)提供了丰富的查询功能

     支持二进制数据及大型对象

     可以根据数据的特点替代RDBMS,成为独立的数据库。或者配合RDBMS,存储特定的数据

# Redis

## 1. 概述

   默认端口：6379

   前台启动：redis-server

   后台启动：redis-server /xxxxx/xxx/redis.conf

   客户端连接：redis-cli [-c] [-p 6379 or portnum]：-c 以集群方式连接 ,退出：shutdow

   redis默认16个数据库，默认使用0号数据库，通过select 0-15进行切换

   开源的key-value存储系统

   支持多种value数据类型：string(字符串)、list(链表)、set(集合)、zset(sorted set有序集合)、hash

   这些数据类型都支持push/pop，add/remove及取交集并集和差集等丰富的操作，且这些操作都是原子性的；还支持排序

   为了保证效率，数据都存在内存里面，Redis会周期性的把更新的数据写入磁盘或者把修改操作写入追加的记录文件

   实现master-slave(主从)同步

## 2. 应用场景

   做高速缓存：如：session共享

   多样的数据结构存储持久化数据

## 3. 单线程+多路IQ复用

   与Memcache(多线程+锁机制)的几点不同：支持多种数据类型，支持持久化，单线程+多路IO复用

   多路复用：使用一个线程来检查多个文件描述符(Socket)的就绪状态，一个操作如果有一个文件描述符就绪则返回，否咋阻塞直到超时。得到就绪状态后进行真正的操作可以在同一个线程里面执行，也可以启动线程执行

## 4. 常用Value的数据类型及操作

   ### key的操作

     **keys ***：查看当前库的所有key
    
     **exists key**：判断key是否存在
    
     **type key**：查看key类型
    
     **del key**：删除指定key数据
    
     **unlink key**：根据value选择非阻塞删除，仅将key从keyspace元数据中删除，真正的删除会在后续异步操作
    
     **expire key 10**：为key设置过期时间，单位秒
    
     **ttl key**：查看还要多久过期，-1：永不过期；-2：已经过期；不手动设置过期时间就表示永不过期
    
     **dbsize**：查看当前库key的数量
    
     **flushdb**：清空当前库
    
     **flushall**：清空所有的库

   ### string——字符串

string是redis中最基本的数据类型：key-value形式。

 string类型是二进制安全的，意味着可以包含任何能够转换为字符串的数据。

 一个redis字符串value最多可以是**512M**

 - 命令

   1. set key value：添加键值对

      如果设置key存在，会覆盖当前的值

   2. get key：获取

   3. append key value1：将value1追加到当前的value中

   4. strlen key：获取key值的长度

   5. setnx key value：只有key不存在时，才能设置key-value

   6. incr key：将value的值加1；**value只能是数字类型，如果为空，则结果为1**

      特点：操作是原子性的(原子操作指不会被线程调度机制打断的操作)，因为redis是单线程的

      - 在单线程中，能够在单条指令中完成的操作都是原子性的，因为中断只能发生在指令之间
      - 在多线程中，不能被其他线程打断的操作叫做原子操作

   7. decr key：将value的值减1；**value只能是数字类型，如果为空，则结果为-1**

   8. incrby/decrby key step：每次value的值加/减step

   9. mset key1 value1 key2 value2：同时设置多个key-value

   10. mget key1 key2 key3：同时获取多个value

   11. msetnx key1 value1 key2 value2：当key不存在时才能设置成功。原子性，有一个失败就都失败

   12. getrange key index1 index2：从下标index1到index2截取出value值

   13. setrange key index1 value：从下标index1开始赋值为value（会发生覆盖）

   14. setex key time value：在设置值的同时设置过期时间

   15. getset key newValue：获取原来的value值，同时赋值为新的newValue

 - 数据结构

   string的数据结构为简单动态字符串，预先分配一定空间，空间不足时会扩容，最大为512M

### List——列表

单键多值

 简单的字符串列表，按照插入顺序排序。可将元素添加到列表的头部和尾部。

 实际是一个双向链表，对两端操作性能高，通过索引操作中间性能较低

 - 命令

   1. lpush/rpush key value value1 value2：从左边/右边插入多个值
   2. lpop/rpop key：从左边/右边出值，key还有值，key就存在；key没有值了，key就不在了
   3. lrange key index1 index2：按下标获取元素
   4. lindex key index：根据索引获取元素，从左到右
   5. llen key：获取列表长度
   6. linsert key before/after value newValue：在value前/后插入newValue值
   7. lrem key n value：从左边删除n个value
   8. lset key index newValue：将下标index的值替换为newValue

 - 数据结构

   list的数据结构为快速链表quickList。

   首先在列表元素较少的情况下会使用会使用一块连续的内存存储，即压缩列表(ziplist)。所有元素挨在一起存储，分配一块连续的内存。因为普通的链表需要的附加指针空间太大，会比较浪费空间

   数据量较多的时候才会使用快速链表：链表+ziplist结合组成了快速链表，就是将多个ziplist使用双向指针串起来使用，这样既满足了插入删除的性能，又不会有太大的空间冗余

### set——集合

功能与list去重，特殊在于set可以自动去重

 set是string类型的无序集合，底层是一个value为null的hash表，所以添加，删除，查找的复杂度都是O(1)

 - 命令
   1. sadd key value value1 value3：添加多个值
   2. smembers key：取出key的所有值
   3. sismember key value：判断在key里面value值是否存在，存在返回1，否则0
   4. scard key：返回集合的元素个数
   5. srem key value value1 ：删除集合中的元素
   6. spop key：随机取出一个值，取完了值，key就不存在了
   7. srandmember key n：随机从集合中取出n个值，值不会从集合中删除
   8. smove sourceKey destinationKey value：把集合中的一个值从一个集合移动到另一个集合内,值如果事先存在就不会添加，因为不能重复
   9. sinter key1 key2：返回交集
   10. sunion key1 key2：返回并集
   11. sdiff key1 key2：返回差集；key1中存在，key2中不存在的
   
 - 数据结构

   通过dict字典实现，字典是哈希表实现的

### Hash——哈希

hash是一个键值对集合

hash是一个string类型的field和value的映射表，hash特别适合用于存储对象；类比于：<string,object>

 - 命令
   1. hset key field value：给key哈希集合中的field字段赋值为value
   2. hget key field：取出key哈希集合中的field字段的值
   3. hmset key field1 value1 field2 value2：给key哈希集合的多个字段设置值
   4. hexists key field：查看key哈希集合中，某字段是否存在
   5. hkeys key：取出key哈希集合中的所有字段
   6. hvals key：取出key哈希集合中的所有value值
   7. hincrby key field num：给哈希集合中的某字段加增量num
   8. hsetnx key field value：当字段不存在时设置值
   
 - 数据结构

   数据结构有两种：压缩列表(ziplist)，哈希表(hashtable)

   当field-value所对应的对象长度较短且个数较少时会使用压缩列表

###  Zset——有序集合

功能于set集合类似，只是可以排序

- 命令

  1. zadd key score1 value1 score 2 value2：将元素以及其score加到集合；按照score排序(从小到大)
  2. zrange key start stop [withscores]：从下标start到stop查看数据；(0,-1)：取出全部数据；withscores：同时返回score
  3. zrangebyscore key minscore maxscore [withscore]：根据score范围查看数据
  4. zrevrangebyscore key maxscore minscore [withscore]：根据score范围查看数据，从大到小
  5. zincrby key num value：给元素的score增加num
  6. zrem key value：删除指定value
  7. zcount key min max：统计score范围内元素的个数
  8. zrank key value：返回该值在集合中的排名，从0开始

- 数据结构

  hash：value对应hash里面的field；score对应hash里面的value

  跳跃表

### Bitmaps

用位来表示value

比如 value的值为a，a所对应的ASCII吗为97，那么用二进制表示为：01100001

合理的使用操作位能够有效的提高内存使用率和开发效率

注：redis6提供Bitmaps这个数据类型，可以实现对位的操作：

1. bitmaps本身不是一种数据类型，实际上他就是字符串，但是可以对字符串进行位操作
2. bitmaps单独提供了一套命令，所以使用bitmaps和使用字符串的方式不一样。可以理解为：bitmaps是一个以位为单位只存储0和1的数组，数组的下标叫做偏移量

- 命令
  1. setbit key offset value：设置key中bitmaps中某个偏移量(下标)的值
  2. getbit key offset：取某个偏移量位置的值
  3. bitcount key [start end]：统计start到end位置的设置为1的数量
  4. bitop and/or/not/xor destkey key1 key2：key1 key2复合操作：交集、并集、非、异或操作，将结果保存到destkey中

### HypeyLogLog

用来计算基数(如：{1，2，3，4，4}，基数集为：{1，2，3，4}，基数为：4)统计的算法，优点是在输入元素的数量或者体积非常大时，计算基数所需要的空间总是固定的，并且时很小的。

每个hyperLogLog键只需要花费12k内存，就可以直接计算2^64个不同元素的基数

- 命令
  1. pfadd key value1 [value2 value3]：给key设置value，重复的value不会添加
  2. pfcount key [key2 key3]：计算近似基数
  3. pfmerge destkey sourcekey1 sourcekey2：将key1和key2合并到destkey中

### Geogrphic

地理信息，该类型就是元素的2为坐标，在地图上就是经纬度。redis基于该类型，提供了经纬度设置、查询、范围查询、距离查询、经纬度hash等操作

- 命令

  1. geoadd key long lat member [long lat member]：给key添加地理位置信息(经度，维度，名称)

     经度：[-180,180]

     维度:[-85.05112878，85.05112878]

  2. geopos key member：获取某名称取位置

  3. geodist key member1 member2：取两位置的直线距离

  4. georadius key long lat radius：取出以指定的经纬度为圆心，半径为radius范围内的位置

## 配置文件：redis.conf

配置大小单位，只支持byte类型，不支持bit；且大小写不敏感

### NetWork——网络配置

- bind

  默认bind=127.0.0.1只能接受本机的访问请求；不写时无限制接受任何IP的访问。

  服务器需要远程访问就得注释掉。

  如果开启了protected-mode，那么在没有设定bind ip且没有密码时，只允许本机访问

- tcp-backlog

  设置tcp得backlog，backlog是一个连接队列；backlog队列总和=未完成三次握手队列+已完成三次握手队列。

  在高并发环境下设置该值来避免慢客户端得连接问题

- timeout

  客户端连接不操作的超时时间，单位：s；默认为0表示永不超时

- tcp-keepalive

  心跳；单位：s；默认300

### General——通用

- daemonize

  是否开启后台启动；

### Limits——限制

- maxclients

  设置可以同时与多少个客户端进行连接

  默认情况下为10000个客户端，如果连接数达到了此限制，会拒绝新的连接请求，并向这些请求方发出以达到最大连接数的响应

- maxmemory

  最大内存，内存满会宕机

  如果内存达到限制，redis会试图移除内部数据，移除规则通过maxmemory-policy来指定

  移除规则：

  - volatile-lru：使用LRU算法(最近最久未使用算法)移除key，只对设置了过期时间的键；最近最少使用
  - allkeys-lru：在所有集合key中，使用LRU算法移除key
  - volatile-random：在过期集合中随机的移除key，只针对设置了过期时间的key
  - allkeys-random：在所有集合key中，随机的移除key
  - volatile-ttl：移除那些TTL值最小的key，即那些最近就要过期的key
  - noeviction：不进行移除。针对写操作，只是返回错误的信息

- maxmemory-samples

  设置redis在移除数据是一次选择多大的数据。

  > 设置样本数量，redis默认会检查多少个key来进行LRU和TTL的选择

​       一般设置3~7的数字，数字越小性能消耗越小，但是越不准确。

### 持久化

- appendonly：是否开启AOF持久化，默认no不开启
- 

## 发布订阅

### 概述

 发布订阅是一种消息通信模式：发送者(pop)发送消息，订阅者(sub)接收消息。

redis客户端可以定义任意数量的频道(类似于kafa的tip)

### 命令

- subscribe channelName：订阅名字为channelName的频道

- publish channelName msg：向名字为channelName的频道发送消息msg

  没有订阅的消息不能持久化，没有订阅就接收不到

## 事务和锁

### 事务

redis事务是一个单独隔离的操作：事务中的所有命令都会被序列化，按顺序的执行，事务在执行的过程中，不会被其他客户端发送的命令请求打断

redis事务主要作用：串联多个命令防止别的命令插队

- Multi、Exec、discard

  1. Multi（开始组队）：开启事务，输入此命令之后，再输入的命令都会依次进入命令队列中，但不会被执行
  2. Exec（执行命令）：输入此命令之后，会依次执行命令队列里面的命令
  3. discard：放弃命令执行

- 错误处理

  Multi期间：命令队列中某个命令出现了错误，执行时中各队列都会被取消

  Exec期间：某个命令出错，只有出错的命令不会被执行，其余的命令正常执行，不会回滚

- 事务的冲突问题

  1. 悲观锁

     认为每次取数据的时候都有别人会修改数据，所以每次拿数据的时候都会上锁。传统的关系型数据库里边就有很多这种锁机制，如：表锁，行锁，读锁，写锁等，都是在操作数据前先上锁

  2. 乐观锁

     认为每次取数据的时候别人都不会修改数据，所以每次取数据不会上锁，但是要通过给数据加版本号等方式来判断数据是否有更新(check-and-set机制来实现事务)。每个操作在更新数据的同时需要取修改版本号

     乐观锁适用于多读的应用类型，可提供吞吐量

  3. 命令 watch key [key1 key2]

     在执行multi命令之前，先执行watch key命令来监视一个key或多个key，如果在事务执行之前这些key被其他命令改动，那么事务就被打断

     使用的是乐观锁

  4. 命令 unwatch key

     取消对key的监视

- redis事务的特性

  1. 单独的隔离操作：事务中的所有命令都会被序列化，按顺序的执行。事务在执行的过程中，不会被其他客户端打断
  2. 没有隔离级别的概念：队列中的命令没有提交之前都不会实际被执行，因为事务提交前任何命令都不会被执行
  3. 不保证原子性：事务执行期间如果一条命令执行失败，其后的命令依旧会被执行，没有回滚

- LUA脚本

  1. 在redis中使用的优势

     将复杂的或者多步的redis操作写成一个脚本，一次提交给redis执行，减少反复连接redis的次数，提升性能

     Lua脚本类似于redis的事务，有一定的原子性，不会被其他命令插队，可以完成一些redis事务性的操作

     redis的lua脚本功能，只有下2.6以上才可以使用，可以利用lua脚本淘汰用户，解决超卖问题

     通过lua脚本解决争抢问题实际上是利用其单线程的特性，用任务队列的方式解决多任务并发的问题

## 持久化

### RDB

- 概述

  在指定时间间隔内将内存中的数据集快照写入磁盘，恢复时将快照读到内存里

- 如何进行备份的

  redis会单独创建fork一个子进程来进行持久化，会先将数据写入到一个临时文件中，待持久化过程都结束后，在用这个临时文件(保证数据的安全性，方式redis服务宕机)替换上次持久化好的文件。整个过程中，主进程不进行任何IO操作，这就确保了极高的性能，如果需要进行大规模的数据恢复，且对于数据恢复的完整性不是非常敏感，那RDB方式要比AOP方式更高效。RDB的缺点是最后一次持久化后的数据可能会丢失。

  **如果持久化的时间间隔配置为：如果30s修改10个key就会持久化一次，那么30s内修改了17个key，那么第一个30s只会持久化10个key，剩余的key进行新的一轮30s计时**

- Fork

  fork的作用是复制一个与当前进程一样的进程。新的进程的所有数据都和原进程一致，但是一个新的进程并作为原进程的子进程

  > 在Linux程序中，fork会产生一个和父进程完全相同的子进程，但子进程在此后多会exec系统调用，处于效率考虑，Linux中引入了“写时复制技术”

  > 一般情况父进程和子进程会共用一段物理内存，只有进程空间的各段的内容要发生变化时，才会将父进程的内容复制一份给子进程

- dump.rdb文件

  持久化的文件，存储的位置是当前的启动目录，可通过配置文件修改

- 优点和缺点

  - 优点

    适合大规模的数据恢复

    对数据完整性和一致性要求不高的更适合使用

    节省磁盘空间

    恢复速度快

  - 缺点

    fork的时候，内存中的数据被克隆了一份

    虽然redis在fork时使用了写时复制的基数，但是如果数据量多时还是比较消耗性能

    在备份周期在一定间隔做一次备份，所有如果redis意外宕机，可能会丢失一部分数据

### AOF

- 概述

  以日志的形式来记录每个写操作(增量保存)，将redis执行过的所有**写指令**记录下来，只许追加文件但不可以改写文件，redis启动之初会读取该文件重新构建数据，即：redis重启就会根据日志文件的内容将写指令从前到后执行一遍来恢复数据

- 持久化流程

  1. 用户的请求写命令被追加到AOF缓冲区
  2. AOF缓冲区根据AOF持久化策略(always、everysec、no)将操作同步到磁盘的aof文件中
  3. AOF文件大小超过重写策略或手动重写时，会对AOF文件重写，压缩AOF文件容量
  4. Redis重启时，会加载aof文件进行数据恢复

  同步频率设置：

  - appendfsync always

    始终同步，每次redis的写入指令都会被立刻记录进日志，性能比较差，数据完整性好

  - appendfsync everysec

    每秒同步，每秒钟计入日志一次，如果宕机，本秒的数据可能丢失

  - appendfsync no

    redis不主动进行同步，把同步时机交给操作系统

- AOF默认不开启

  持久化日志文件：appendonly.aof

  存储路径默认为当前启动路径

  - 如果AOF和RDB同时开启

    如果同时开启，系统会默认读取AOF的数据(数据不存在丢失)

  - AOF可以进行异常恢复

    如果aof文件损坏，通过指令：redis-check-aof --fix .aofFileName进行恢复

- 优点和缺点

  - 优点：

    备份机制更稳健，丢失数据概率更低

    可读的日志文本，通过操作AOF文件，可以处理误操作

  - 缺点

    比RDB更占空间

    恢复备份慢

    每次读写都同步，有一定性能压力

## 主从复制

### 概述

主机数据更新后根据配置和策略，自动同步到备机的master/slaver机制。master以写为主，slaver以读为主

特点：

1. 读写分离
2. 从机宕机后，快速恢复

### 配置从机

- slaveof 主机ip 主机port：为主机配置从机
- info repliaction：查看主机信息

### 常用方式

- 一主多从

  从服务器宕机之后，再次开启，主从关系消失，需要重新配置主服务器

  主服务器宕机之后，从机依旧是从机，重启主服务器依旧是主服务器，之前的从机依旧是主服务器的从机

- 薪火相传(像一棵树)

  上一个slave可能是下一个slave的master，slave同样可以接收其他slave的连接和同步请求，那么该slave作为了链条中下一个的master，可以有效减轻master的写压力，去中心化降低风险

- 反客为主

  当一个master宕机之后，后面的slave可以立刻被提升为master，其后面的slave不做任何修改

  slaveof no one：从机变主机

### 哨兵模式

- 概述

  反客为主的自动版，能够后台监控主机是否故障，如果故障了根据投票自动将从机转换为主机

  如果之前宕机的主机重启了，会被作为一个从机运行

- 复制延时

  由于所有的操作都是现在主机上操作，然后同步更新到从机上，所以主机同步到从机有一定的延迟，当系统繁忙时，延迟问题会更严重；从机数量增加，延迟问题也会变严重

### 复制原理

1. 当从机连接上主机之后，会向主机发送进行数据同步的消息
2. 全量复制：主机接收到数据同步消息，把主机数据进行持久化，并且把rdb文件发送给从机，从机拿到文件读取进行数据恢复
3. 增量复制：每次主机进行写操作后，和从机进行数据同步(主机主动进行数据同步)

## 集群

无中心化集群：每一个主机都可以是请求的入口，每个主机可以互相通信转发请求

- 概述

  redis集群实现了对redis的水平扩容，即启动N个redis节点，将整个数据库分布存储在这N个节点中，每个节点存储总数据的1/N

  redis集群通过分区来提供一定程度的可用性：即使集群中有一部分节点失效或者无法进行通讯，集群也可以继续处理请求

- 集群如何分配节点

  一个集群至少要有三个主节点

  选项：--cluster-replicas 1表示希望为集群中的每个主节点创建一个从节点

  分配原则尽量保证每个主机运行在不同的IP地址，每个从机和主句不在一个IP地址上；为了保证集群的可以性，一台挂了其他可以顶替

- slots

  一个redis集群包含16384个插槽，数据库中的每个键都属于这16384个插槽中的一个，集群使用公式CRC16(key)%16384来计算key属于哪个插槽，其中CRC16(key)语句用于计算key的CRC16校验和

  集群的每个节点负责处理一部分插槽，每个节点只能查看主机插槽范围的值

  作用：可以把请求分担到不同的主节点中

- 故障恢复

  如果某一段插槽的主从都宕机，而配置redis.conf文件中的cluster-require-full-coverage=yes，那么整个集群都会停掉；否则只有宕机的那一段插槽数据不等使用也无法存储

- 优缺点

  - 优点

    实现扩容

    分摊压力

    无中心配置相对简单

  - 缺点

    不支持多键操作(无法计算插槽值)

    多建的redis事务是不被支持的

## 解决的问题

### 缓存穿透

- 概述

  key对应的数据不存在，每次对此key的请求从缓存获取不到，请求的压力都会集中到数据库，从而可能压垮数据库

- 解决方案

  1. 对空值做缓存：如果一个查询返回的数据为空(不管是数据是否不存在)，都把这个空结果进行缓存，设置空结果的过期时间会很短
  2. 设置可访问的名单：使用bitmaps类型定义一个可以访问的名单，名单ID作为bitmaps的偏移量，每次访问都和缓存的id进行比较如果id不存在就不允许访问
  3. 采用布隆过滤器：实际上是一个很长的二进制向量和一系列随机映射函数。布隆过滤器可以用于检索一个元素是否存在一个集合里面。优点是空间效率和查询效率都源超过一般的算法，缺点是有一定的误识别和删除困难
  4. 进行实时监控：当发现redis的命中率开始急速下降，需要排查访问对象和访问的数据，设置黑名单限制服务

### 缓存击穿

- 概述

  key对应的数据在redis中存在，但是过期了，此时如果有大量的请求过来，这些请求发现缓存过期就会去数据库中查询并设置回缓存，这个时候并发的请求可能回瞬间把数据库压垮

- 解决方案

  1. 预先设置热门数据：在redis高峰访问之前把一些热门的数据提前存入到redis里面，加大过期时长
  2. 实时调整：现场监控哪些数据热门，实时调整key的过期时长
  3. 使用锁：
     1. 在缓存失效的时候(判断读取出的值为空)，不是立即去加载数据库
     2. 先使用缓存工具的某些带成功操作返回值的操作去set一个mutex key
     3. 当操作返回成功，在进行加载数据库的操作，并设置缓存，最后删除mutex key
     4. 当操作返回失败，证明有线程在加载数据库，当前线程睡眠一段时间后再重试整个get缓存的方法

### 缓存雪崩

- 概述：

  大量的key在缓存中对应的数据存在，但再redis中过期，此时若有大量数据并发请求过来，请求发现过期就会访问数据胡并添加缓存，这个时候并发的请求可能回瞬间把数据库压垮

- 解决方案
  1. 构建多级缓存架构：nginx缓存+redis缓存+其他缓存
  2. 使用锁或者队列：使用锁或者队列的方式来保证不会有大量的线程随数据库一次性进行读写，从而避免失效时大量的并发请求落到数据库，并不适用高并发的情况
  3. 设置过期标志更新缓存：记录缓存数据是否过期，如果过期会触发通知另外的线程在后台更新实际key的缓存
  4. 将缓存失效的时间分散开：降低缓存集体失效的概率