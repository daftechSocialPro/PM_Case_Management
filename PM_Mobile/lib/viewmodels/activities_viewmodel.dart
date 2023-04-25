import 'package:daf_project1/models/activity.dart';
import 'package:daf_project1/services/api.dart';
import 'package:daf_project1/viewmodels/ui_state.dart';
import 'package:flutter/foundation.dart';

class ActivitiesViewModel extends ChangeNotifier{
  late Api _api;
  late List<Activity> activities;
  late UiState activitiesUiState;
  String errorMessage = "";
  ActivitiesViewModel(Api api){
    _api = api;
    _initActivities();
  }
  _initActivities() async{
    setUiState(UiState.onLoading);
    try {
      activities = await _api.getActivities();
      setUiState(UiState.onResult);
    }
    catch(e){
      errorMessage = e.toString();
      setUiState(UiState.onError);
    }
  }
  void setUiState(UiState uiState){
    activitiesUiState = uiState;
    notifyListeners();
  }

  Future<void> getActivities() async{
     _initActivities();
  }

}