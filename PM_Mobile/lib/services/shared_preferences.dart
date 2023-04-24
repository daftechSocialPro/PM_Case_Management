
import 'package:shared_preferences/shared_preferences.dart';

class SharedPreferencesUtil{
   static SharedPreferences? _preferences;

  static void initPreference() async{
    if(_preferences == null){
      _preferences =  await SharedPreferences.getInstance();
    }
  }

  SharedPreferencesUtil(){
    initPreference();
  }
  setPublishName(String publishName){
    print("Saving publishName.....");
    _preferences!.setString("publishName", publishName);
    print("PublishName from preference: ${getPublishName()}");

  }
  setIpAddress(String ipAddress){
      print("Saving ipAddress.....");
      _preferences!.setString("ipAddress", ipAddress);
      print("IpAddress from preference: ${getIpAddress()}");
      print("PortNumber from preference: ${getPortNumber()}");

  }

  setPortNumber(int port){
    print("Saving port number....");
    _preferences!.setInt("portNumber", port);
  }

  String getIpAddress(){
    if(_preferences != null){
      return _preferences!.getString('ipAddress') ?? "localhost";
    }
    return "";

  }
  String getPublishName(){
    if(_preferences != null){
      return  _preferences!.getString('publishName') ?? "";
    }
    return "";

  }
  int getPortNumber(){
    if(_preferences != null){
      return _preferences!.getInt('portNumber') ?? 0;
    }

    return 0;
  }

}