# MongoDB 性能优化指南

## 索引设计

### 创建索引

```javascript
// 单字段索引
db.users.createIndex({ username: 1 })

// 复合索引
db.posts.createIndex({ author: 1, createdAt: -1 })

// 文本索引
db.articles.createIndex({ content: "text" })
```

### 索引选择原则

1. 查询字段建索引
2. 排序字段考虑索引
3. 避免过度索引（影响写入性能）

## 查询优化

### 使用 Explain 分析

```javascript
db.users.find({ age: { $gt: 30 } }).explain("executionStats")
```

### 覆盖查询

```javascript
// 好 - 使用覆盖索引
db.users.find({ age: 25 }, { name: 1, _id: 0 })

// 不好 - 需要回表查询
db.users.find({ age: 25 })
```

## 分片策略

### 分片键选择

| 场景 | 分片键 |
|------|--------|
| 时间序列数据 | 时间字段 |
| 用户数据 | 用户ID |
| 地理位置 | 地理坐标 |

## 监控指标

- 操作延迟
- 连接池使用率
- 缓存命中率
- 锁等待时间
