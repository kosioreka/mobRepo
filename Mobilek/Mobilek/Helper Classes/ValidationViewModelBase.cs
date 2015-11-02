using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.ComponentModel;

namespace Mobilek
{
    public class ValidationViewModelBase : ViewModelBase, IDataErrorInfo
    {
        private Dictionary<string, Binder> ruleMap = new Dictionary<string, Binder>();

        public void AddRule<T>(Expression<Func<T>> expression, Func<bool> ruleDelegate, string errorMessage)
        {
            var name = GetPropertyName(expression);

            ruleMap.Add(name, new Binder(ruleDelegate, errorMessage));
        }

        protected override void Set<T>(Expression<Func<T>> path, T value, bool forceUpdate)
        {
            ruleMap[GetPropertyName(path)].IsDirty = true;
            base.Set<T>(path, value, forceUpdate);
        }

        public bool HasErrors
        {
            get
            {
                var values = ruleMap.Values.ToList();
                values.ForEach(b => b.Update());

                return values.Any(b => b.HasError);
            }
        }

        public string Error
        {
            get 
            {
                var errors = from b in ruleMap.Values where b.HasError select b.Error;

                return string.Join("\n", errors); 
            }
        }

        public string this[string columnName]
        {
            get 
            {
                if (ruleMap.ContainsKey(columnName))
                {
                    ruleMap[columnName].Update();
                    return ruleMap[columnName].Error;
                }
                return null;
            }
        }


        private class Binder
        {
            private readonly Func<bool> ruleDelegate;
            private readonly string message;

            internal Binder(Func<bool> ruleDelegate, string message)
            {
                this.ruleDelegate = ruleDelegate;
                this.message = message;

                IsDirty = true;
            }

            internal string Error { get; set; }
            internal bool HasError { get; set; }

            internal bool IsDirty { get; set; }

            internal void Update()
            {
                if (!IsDirty)
                    return;

                Error = null;
                HasError = false;
                try
                {
                    if (!ruleDelegate())
                    {
                        Error = message;
                        HasError = true;
                    }
                }
                catch (Exception e)
                {
                    Error = e.Message;
                    HasError = true;
                }
            }
        }
    }



    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Dictionary<string, object> propertyValueMap;

        protected ViewModelBase()
        {
            propertyValueMap = new Dictionary<string, object>();
        }

        private void InternalPropertyUpdate(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void OnPropertyChanged<T>(Expression<Func<T>> path)
        {
            string propertyName = GetPropertyName(path);
            InternalPropertyUpdate(propertyName);
        }


        protected T Get<T>(Expression<Func<T>> path)
        {
            return Get(path, default(T));
        }

        protected virtual T Get<T>(Expression<Func<T>> path, T defaultValue)
        {
            var propertyName = GetPropertyName(path);
            if (propertyValueMap.ContainsKey(propertyName))
            {
                return (T)propertyValueMap[propertyName];
            }
            else
            {
                propertyValueMap.Add(propertyName, defaultValue);
                return defaultValue;
            }
        }

        protected void Set<T>(Expression<Func<T>> path, T value)
        {
            Set(path, value, false);
        }

        protected virtual void Set<T>(Expression<Func<T>> path, T value, bool forceUpdate)
        {
            var oldValue = Get(path);
            var propertyName = GetPropertyName(path);

            if (!object.Equals(value, oldValue) || forceUpdate)
            {
                propertyValueMap[propertyName] = value;
                OnPropertyChanged(path);
            }
        }

        protected static string GetPropertyName<T>(Expression<Func<T>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            Expression body = expression.Body;
            MemberExpression memberExpression = body as MemberExpression;
            if (memberExpression == null)
            {
                memberExpression = (MemberExpression)((UnaryExpression)body).Operand;
            }
            return memberExpression.Member.Name;
        }

    }
}
