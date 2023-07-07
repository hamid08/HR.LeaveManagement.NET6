﻿using HR.LeaveManagement.Application.Utils;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace HR.LeaveManagement.Application.Extensions
{
    public static class EnumExtensions
    {
        public static List<string> ToDisplay(this Enum value, DisplayProperty property = DisplayProperty.Name)
        {
            Assert.NotNull(value, nameof(value));
            List<string> Messages = new List<string>();

            var attribute = value.GetType().GetField(value.ToString())
                .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

            if (attribute == null)
                return Messages;

            var propValue = attribute.GetType().GetProperty(property.ToString()).GetValue(attribute, null);
            Messages.Add(propValue.ToString());
            return Messages;
        }
    }

    public enum DisplayProperty
    {
        Description,
        GroupName,
        Name,
        Prompt,
        ShortName,
        Order
    }
}
