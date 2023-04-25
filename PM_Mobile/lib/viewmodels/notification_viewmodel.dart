import 'package:daf_project1/models/activity.dart';
import 'package:daf_project1/models/activity_progress.dart';
import 'package:daf_project1/models/notification.dart';
import 'package:daf_project1/services/api.dart';
import 'package:daf_project1/viewmodels/ui_state.dart';
import 'package:flutter/foundation.dart';



class NotificationViewModel extends ChangeNotifier{

  List<Notification>? notifications;
  late Api _api;
  UiState notificationsUiState = UiState.initial;
  String errorMessage = "";
  int notificationCount = -1;

  NotificationViewModel(Api api){
    _api = api;
    _initNotifications();
  }
  _initNotifications() async{
    setUiState(UiState.onLoading);
    try {
      if(_api.coordinatingActivities == null){
        _api.getActivities();
        print("Notification called getActivities()");
      }
      notifications = await _api.getNotifications();
      _getNotificationCount();
      setUiState(UiState.onResult);
    }
    catch(e){
      errorMessage = e.toString();
      setUiState(UiState.onError);
    }
  }
  void setUiState(UiState uiState){
    notificationsUiState = uiState;
    notifyListeners();
  }

  Future<void> getNotifications() async{
    _initNotifications();
  }

  List getProgress(String activityId , progressId){
    Activity activity = _api.coordinatingActivities!.where((element){return element.id == activityId;}).first;
    ActivityProgress progress = activity.progresses.where((element) => element.id == progressId).first;
    return [activity,progress];
  }

  void _getNotificationCount(){
    int counter = 0;
    if(notifications != null) {
      counter = notifications!.length;
    }
    else{
      counter = -1;
    }
    notificationCount = counter;
  }
}