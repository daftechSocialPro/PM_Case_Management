
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
      return _preferences!.getString('ipAddress') ?? "197.156.93.75";
    }
    return "";

  }
  String getPublishName(){
    if(_preferences != null){
      return  _preferences!.getString('publishName') ?? "IPDCS_WC_LATEST";
    }
    return "";

  }
  int getPortNumber(){
    if(_preferences != null){
      return _preferences!.getInt('portNumber') ?? 80;
    }

    return 0;
  }

}