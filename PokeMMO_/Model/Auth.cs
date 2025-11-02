// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Model.Auth
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Mvvm;

#nullable disable
namespace PokeMMO_.Model;

public class Auth : BindableBase
{
  private string _Status = "Status: FREE";
  private string _ID = "Premium ID: ";
  private string _Username = "Username: ";
  private string _Email = "Email: ";
  private string _Rank = "Rank: ";
  private string _HWID = "HWID: ";
  private string _UserVariable = "User Variable: ";
  private string _IP = "IP: ";
  private string _Expiry = "Expiry: ";
  private string _LastLogin = "Last Login: ";
  private string _RegisterDate = "Register Date: ";

  public string Status
  {
    get => this._Status;
    set => this.SetProperty<string>(ref this._Status, value, nameof (Status));
  }

  public string ID
  {
    get => this._ID;
    set => this.SetProperty<string>(ref this._ID, value, nameof (ID));
  }

  public string Username
  {
    get => this._Username;
    set => this.SetProperty<string>(ref this._Username, value, nameof (Username));
  }

  public string Email
  {
    get => this._Email;
    set => this.SetProperty<string>(ref this._Email, value, nameof (Email));
  }

  public string Rank
  {
    get => this._Rank;
    set => this.SetProperty<string>(ref this._Rank, value, nameof (Rank));
  }

  public string HWID
  {
    get => this._HWID;
    set => this.SetProperty<string>(ref this._HWID, value, nameof (HWID));
  }

  public string UserVariable
  {
    get => this._UserVariable;
    set => this.SetProperty<string>(ref this._UserVariable, value, nameof (UserVariable));
  }

  public string IP
  {
    get => this._IP;
    set => this.SetProperty<string>(ref this._IP, value, nameof (IP));
  }

  public string Expiry
  {
    get => this._Expiry;
    set => this.SetProperty<string>(ref this._Expiry, value, nameof (Expiry));
  }

  public string LastLogin
  {
    get => this._LastLogin;
    set => this.SetProperty<string>(ref this._LastLogin, value, nameof (LastLogin));
  }

  public string RegisterDate
  {
    get => this._RegisterDate;
    set => this.SetProperty<string>(ref this._RegisterDate, value, nameof (RegisterDate));
  }
}
