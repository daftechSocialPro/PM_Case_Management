
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
    String ipAddress = _preferences!.getString('ipAddress') ?? "localhost";
    return ipAddress;

  }
  int getPortNumber(){
    int portNumber = _preferences!.getInt('portNumber') ?? 0;
    return portNumber;
  }

}