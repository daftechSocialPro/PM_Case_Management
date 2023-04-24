import 'package:daf_project1/services/shared_preferences.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/foundation.dart';

class SettingsViewModel extends ChangeNotifier{

  late SharedPreferencesUtil _preferencesUtil;
  late String _ipAddress;
  late String _port;
  late String _publishName;
  late TextEditingController ipAddressController;
  late TextEditingController portNumberController;
  late TextEditingController publishNameController;

  String get ipAddress => _ipAddress;

  set ipAddress(String value) {
    _ipAddress = value;
  }

  SettingsViewModel(preferencesUtil){
    _preferencesUtil = preferencesUtil;
    _ipAddress = preferencesUtil.getIpAddress();
    _port = preferencesUtil.getPortNumber().toString();
    _publishName = preferencesUtil.getPublishName();
    ipAddressController = new TextEditingController(text: _ipAddress);
    portNumberController = new TextEditingController(text: _port);
    publishNameController = new TextEditingController(text: _publishName);


  }

  void saveSettings(){
    String portStr = portNumberController.text.trim();
    int port = int.parse(portStr);

    _port = portStr;
    _ipAddress = ipAddressController.text.trim();
    _publishName = publishNameController.text.trim();
    _preferencesUtil.setIpAddress(ipAddress);
    _preferencesUtil.setPublishName(_publishName);
    _preferencesUtil.setPortNumber(port);
  }

  void getSettings(){
    this._ipAddress = _preferencesUtil.getIpAddress();
    this._port = _preferencesUtil.getPortNumber().toString();
  }

  String get port => _port;

  set port(String value) {
    _port = value;
  }
}