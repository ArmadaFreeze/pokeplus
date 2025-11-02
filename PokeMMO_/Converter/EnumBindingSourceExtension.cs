// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Converter.EnumBindingSourceExtension
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using System;
using System.Windows.Markup;

#nullable disable
namespace PokeMMO_.Converter;

public class EnumBindingSourceExtension : MarkupExtension
{
  public Type EnumType { get; private set; }

  public EnumBindingSourceExtension(Type enumType)
  {
    this.EnumType = ((object) enumType == null ? 1 : (!enumType.IsEnum ? 1 : 0)) == 0 ? enumType : throw new Exception("EnumType is null or not EnumType");
  }

  public override object ProvideValue(IServiceProvider serviceProvider)
  {
    return (object) Enum.GetValues(this.EnumType);
  }
}
