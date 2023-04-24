import 'package:daf_project1/models/notification.dart';
import 'package:daf_project1/services/api.dart';
import 'package:flutter/foundation.dart';



class NotificationViewModel extends ChangeNotifier{

  List<Notification>? notifications;
  late Api _api;
  NotificationViewModel(Api api){
    this._api = api;
    _api.getNotifications().then((value) => notifications = value);
  }

  Future<List<Notification>> getNotifications() async{
    return _api.getNotifications().then((value) => notifications = value);
  }

  int getNotificationCount(){
    int notificationCount = 0;
    if(notifications != null) {
      notifications!.forEach((element) {
        notificationCount += element.description.length;
      });
    }
    return notificationCount;
  }
}