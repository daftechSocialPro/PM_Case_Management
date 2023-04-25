import 'package:device_info_plus/device_info_plus.dart';
import 'dart:io' show Platform;


class MacGetter{

  String? _macAddress;


  String? get macAddress => _macAddress;

  Future<void> initMacAddress() async {
    DeviceInfoPlugin deviceInfo = DeviceInfoPlugin();

    if (Platform.isAndroid) {
      AndroidDeviceInfo androidInfo = await deviceInfo.androidInfo;
      String? androidMacAddress = androidInfo.androidId ;
      _macAddress = androidMacAddress;
      print('Running on Android $androidMacAddress');  // e.g. "Moto G (4)"
    } else if (Platform.isIOS) {
      IosDeviceInfo iosInfo = await deviceInfo.iosInfo;
      String? iosMacAddress = iosInfo.identifierForVendor;
      _macAddress = iosMacAddress;
      print('Running on Ios $iosMacAddress');
    }



  }
}