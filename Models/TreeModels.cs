using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DeepSeek_v4_for_VisualStudio.Models
{
    /// <summary>
    /// 对话树的持久化数据结构（版本 2）。
    /// 以扁平化节点列表存储，通过 parentId/childrenIds 维护父子关系。
    /// </summary>
    [DataContract]
    public class TreePersistenceData
    {
        /// <summary>数据格式版本号（2 = 树状结构）</summary>
        [DataMember]
        public int Version { get; set; } = 2;

        /// <summary>当前活跃分支的叶子节点 ID</summary>
        [DataMember]
        public string? ActiveLeafId { get; set; }

        /// <summary>所有节点的扁平列表</summary>
        [DataMember]
        public List<TreeNodeData> Nodes { get; set; } = new();
    }

    /// <summary>
    /// 单个树节点的持久化数据。
    /// </summary>
    [DataContract]
    public class TreeNodeData
    {
        /// <summary>节点唯一 ID</summary>
        [DataMember]
        public string Id { get; set; } = string.Empty;

        /// <summary>父节点 ID（root 为 null）</summary>
        [DataMember]
        public string? ParentId { get; set; }

        /// <summary>节点携带的消息（root 为 null）</summary>
        [DataMember]
        public ChatMessage? Message { get; set; }

        /// <summary>子节点 ID 列表（按分支顺序排列）</summary>
        [DataMember]
        public List<string>? ChildrenIds { get; set; }
    }
}
