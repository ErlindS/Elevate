﻿using CommunityToolkit.Mvvm.ComponentModel;
using Elevate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Elevate.Models
{
    /// <summary>
    /// Basic Model for tasks/groups
    /// </summary>
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(TaskModel), "task")]
    [JsonDerivedType(typeof(GroupTaskModel), "groupTask")]
    public abstract class BaseTaskModel : ITaskModel
    {
        public virtual string Name { get; protected set; } = "Unnamed";
        public virtual string Description { get; protected set; } = "No description";
        public virtual double Duration { get; protected set; } = 0;
        public abstract int Id { get; }

        public virtual string GetName() => Name;
        public virtual string GetDescription() => Description;
        public virtual double GetDuration() => Duration;
        public virtual int GetId() => Id;
    }
}
