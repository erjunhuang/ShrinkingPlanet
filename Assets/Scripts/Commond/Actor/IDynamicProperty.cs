namespace YanlzFramework
{
    public interface IDynamicProperty
    {
        void DoChangeProperty(EnumPropertyType propertyType, object oldValue, object newValue);

        PropertyItem GetProperty(EnumPropertyType propertyType);
    }
}