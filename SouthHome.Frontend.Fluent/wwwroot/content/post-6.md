# Docker 容器化部署指南

## Docker 基础概念

### 核心组件

- **镜像（Image）**：只读的应用程序包
- **容器（Container）**：镜像的运行实例
- **仓库（Registry）**：存储和分发镜像

## Dockerfile 最佳实践

### 示例 Dockerfile

```dockerfile
# 使用官方基础镜像
FROM node:18-alpine

# 设置工作目录
WORKDIR /app

# 复制依赖文件
COPY package*.json ./

# 安装依赖
RUN npm ci --only=production

# 复制应用代码
COPY . .

# 暴露端口
EXPOSE 3000

# 运行应用
CMD ["node", "index.js"]
```

### 优化建议

1. 使用 Alpine 镜像减小体积
2. 合并 RUN 指令减少层数
3. 利用构建缓存优化构建时间
4. 多阶段构建减小最终镜像大小

## Docker Compose

### 示例配置

```yaml
version: '3.8'

services:
  app:
    build: .
    ports:
      - "3000:3000"
    depends_on:
      - db
      - redis

  db:
    image: postgres:15
    environment:
      POSTGRES_PASSWORD: secret
    volumes:
      - db-data:/var/lib/postgresql/data

  redis:
    image: redis:7-alpine

volumes:
  db-data:
```

## 总结

Docker 极大简化了应用的部署和管理，是现代 DevOps 的基础技术。
