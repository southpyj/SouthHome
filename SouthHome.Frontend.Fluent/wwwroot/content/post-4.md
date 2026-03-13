# Java 并发编程实战

## 线程池使用

### ThreadPoolExecutor 配置

```java
ThreadPoolExecutor executor = new ThreadPoolExecutor(
    4,                          // 核心线程数
    10,                         // 最大线程数
    60L, TimeUnit.SECONDS,      // 空闲线程存活时间
    new LinkedBlockingQueue<>(100),  // 任务队列
    new ThreadPoolExecutor.CallerRunsPolicy()  // 拒绝策略
);
```

## 锁机制

### Synchronized vs ReentrantLock

| 特性 | Synchronized | ReentrantLock |
|------|-------------|---------------|
| 使用方式 | 关键字 | API 调用 |
| 公平锁 | 不支持 | 支持 |
| 超时获取 | 不支持 | 支持 |
| 中断获取 | 不支持 | 支持 |

### 代码示例

```java
// ReentrantLock 示例
ReentrantLock lock = new ReentrantLock();

try {
    if (lock.tryLock(1, TimeUnit.SECONDS)) {
        try {
            // 业务逻辑
        } finally {
            lock.unlock();
        }
    }
} catch (InterruptedException e) {
    Thread.currentThread().interrupt();
}
```

## 并发集合

- **ConcurrentHashMap**：分段锁机制
- **CopyOnWriteArrayList**：写时复制
- **BlockingQueue**：阻塞队列

## 最佳实践

1. 尽量使用高层并发工具（Executor、ForkJoinPool）
2. 避免在同步块中执行耗时操作
3. 注意死锁预防
4. 合理设置线程池参数
