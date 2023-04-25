import 'package:daf_project1/models/employee.dart';
import 'package:daf_project1/services/api.dart';
import 'package:flutter/cupertino.dart';

import '../locator.dart';

class HomeViewModel extends ChangeNotifier{
  Employee? user;
  late Api _api;
  late String imagePath;
  HomeViewModel(Api api){
    _api = api;
    _init();
  }
  _init() {
    user = _api.user;
    imagePath = _api.getRelativeImagePath() + user!.imagePath;
  }

  void signOut(){
     user = null;
    _api.logOut();
  }

}