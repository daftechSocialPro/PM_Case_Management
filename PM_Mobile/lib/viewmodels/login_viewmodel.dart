import 'package:daf_project1/models/employee.dart';
import 'package:daf_project1/services/api.dart';
import 'package:daf_project1/services/mac_getter.dart';
import 'package:daf_project1/viewmodels/ui_state.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:daf_project1/utils/database_helper.dart';


class LoginViewModel extends ChangeNotifier{

  late Api _api;
  UiState loginUiState = UiState.initial;
  late TextEditingController usernameController;
  late TextEditingController passwordController;
  String errorMessage = "";
  bool isUsernameValid = true;
  bool isPasswordValid = true;

  LoginViewModel(Api api){
    this._api = api;
    usernameController = new TextEditingController();
    passwordController = new TextEditingController();
    usernameController.addListener(_usernameListener);
    passwordController.addListener(_passwordListener);
  }

  void _usernameListener() {
    if(usernameController.text.trim().isNotEmpty){
      isUsernameValid = true;
    }
    else{
      isUsernameValid = false;
    }
    notifyListeners();
  }
  void _passwordListener() {
    if(passwordController.text.trim().isNotEmpty){
      isPasswordValid = true;
    }
    else{
      isPasswordValid = false;
    }
    notifyListeners();
  }
  bool isValid(){
    return isUsernameValid && isPasswordValid;
  }


  void login(){
    if(isValid()){


      _login();
    }
  }

  Future<void> _login() async{
    _setUiState(UiState.onLoading);
    String username = usernameController.text.trim();
    String password = passwordController.text.trim();
    try {
      DatabaseHelper ? _dbHelper = DatabaseHelper.instanse;
  var x = await _dbHelper.fetchUser();
  if (x.isEmpty){
       await _api.login(username, password);
  }
  else{
var len= x.length;
     _api.user=x[len-1];
     _api.user?.id=x[len-1].userId.toString();
     
  }

      _setUiState(UiState.onResult);
    }catch(e){
      print(e);
      errorMessage = e.toString();
      _setUiState(UiState.onError);
    }

  }

  void _setUiState(UiState uiState){
    loginUiState = uiState;
    notifyListeners();
  }
  void resetLoginUiState(){
    loginUiState = UiState.initial;
  }
  Employee? getUser(){
    return _api.user;
  }

}
