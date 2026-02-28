using System;
using System.Windows.Markup;

namespace PokeMMO_.Converter;

public class EnumBindingSourceExtension : MarkupExtension
{
	public Type EnumType { get; private set; }

	public EnumBindingSourceExtension(Type enumType)
	{
		if ((object)enumType == null || !enumType.IsEnum)
		{
			throw new Exception("EnumType is null or not EnumType");
		}
		EnumType = enumType;
	}

	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		return Enum.GetValues(EnumType);
	}
}
