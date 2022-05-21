namespace StateLog.AspNetCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class TemplateBuilder
    {
        private Dictionary<string, Func<object>> defaultArguments = new Dictionary<string, Func<object>>
        {
            {"year", () => DateTime.Now.Year},
            {"month", () => DateTime.Now.Month},
            {"day", () => DateTime.Now.Day},
            {"hour", () => DateTime.Now.Hour},
            {"minute", () => DateTime.Now.Minute},
            {"second", () => DateTime.Now.Second},
        };
        private readonly string _template;

        public TemplateBuilder(string stringTemplate) => _template = stringTemplate;
        public TemplateBuilder() { }
        public TemplateBuilder Add(string key, object value)
        {
            defaultArguments.TryAdd(key, () => value);

            return this;
        }
        public string Build() => Build(_template);
        public string Build(string stringTemplate)
        {
            StringBuilder stringBuilder = new StringBuilder(stringTemplate);
            foreach (var item in defaultArguments)
            {
                stringBuilder.Replace("{" + item.Key + "}", item.Value().ToString());
            }
            return stringBuilder.ToString();
        }
        public Dictionary<string, Func<object>> GetAllArguments() => defaultArguments;
    }
}