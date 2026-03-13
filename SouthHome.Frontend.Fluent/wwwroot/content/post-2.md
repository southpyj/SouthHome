# Kubernetes 集群部署最佳实践

## 集群规划

### 节点配置建议

- **Master 节点**：至少 3 台，保证高可用
- **Worker 节点**：根据应用负载动态扩缩容
- **资源预留**：为系统进程预留 20% 资源

## 网络设计

### CNI 选择

| CNI | 适用场景 |
|-----|---------|
| Calico | 网络策略丰富 |
| Flannel | 简单易用 |
| Cilium | 高性能 |

## 存储方案

```yaml
apiVersion: v1
kind: PersistentVolume
metadata:
  name: example-pv
spec:
  capacity:
    storage: 10Gi
  accessModes:
    - ReadWriteOnce
  persistentVolumeReclaimPolicy: Retain
  storageClassName: local-storage
```

## 监控与日志

- **Prometheus**：指标收集
- **Grafana**：可视化展示
- **ELK Stack**：日志聚合分析

## 安全加固

1. 启用 RBAC
2. 网络策略隔离
3. Secret 管理
4. 定期更新补丁
