using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace XafEasyTestDemo.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Task : BaseObject
    { 
        public Task(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        private string description;
        public string Description
        {
            get => description;
            set => SetPropertyValue(nameof(Description), ref description, value);
        }

        private DateTime dueDate;
        public DateTime DueDate
        {
            get => dueDate;
            set => SetPropertyValue(nameof(DueDate), ref dueDate, value);
        }
    }
}