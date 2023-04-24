import 'package:daf_project1/services/api.dart';
import 'package:flutter/cupertino.dart';

class LoginViewModel extends ChangeNotifier{

  late Api _api;

  LoginViewModel(Api api){
    this._api = api;
  }

  void login(String username, String password){
  }

}

enum ButtonState {
  idle,
  loading,
  error
}