
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
    _preferences!.setString("publishName", publishName);

  }
  setIpAddress(String ipAddress){
      _preferences!.setString("ipAddress", ipAddress);


  }

  setPortNumber(int port){
    _preferences!.setInt("portNumber", port);
  }

  String getIpAddress(){
    if(_preferences != null){
      return _preferences!.getString('ipAddress') ?? "192.168.0.15";
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
      return _preferences!.getInt('portNumber') ?? 7250;
    }

    return 0;
  }

}