
import 'package:daf_project1/models/coordinator_activity.dart';
import 'package:daf_project1/models/my_activity.dart';
import 'package:daf_project1/services/api.dart';
import 'package:flutter/foundation.dart';

class ActivitiesViewModel extends ChangeNotifier{
  late Api _api;
  late List<MyActivity> activities;
  ActivitiesViewModel(Api api){
    _api = api;
    _api.getActivities().then((value) => activities = value);

  }
  Future<List<MyActivity>> getTaskActivities(){
     return _api.getActivities();
  }

}