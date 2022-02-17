using Dapper.Contrib.Extensions;
using FluentValidation.Results;
using System;

namespace GSP.API.Core.Models.Base
{
    public abstract class BaseModel
    {
        public int Id { get; private set; }

        public void SetId(int id) => this.Id = id;

        [Computed]
        public ValidationResult ValidationResult { get; set; }

        public abstract void ValidateModel();
    }
}
