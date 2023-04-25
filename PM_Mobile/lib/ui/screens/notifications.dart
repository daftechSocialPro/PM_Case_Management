import 'package:daf_project1/models/notification.dart' as notif;
import 'package:daf_project1/ui/widgets/notification_item.dart';
import 'package:daf_project1/ui/widgets/error_widget.dart';
import 'package:daf_project1/viewmodels/notification_viewmodel.dart';
import 'package:daf_project1/viewmodels/ui_state.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class NotificationPage extends StatelessWidget {
  const NotificationPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
          padding: EdgeInsets.all(16),
          child: _buildNotifications(context),
        );
  }

  Widget _buildNotifications(BuildContext context) {
    return Consumer<NotificationViewModel>(
      builder: (BuildContext context, value, Widget? child) {
        if (value.notificationsUiState == UiState.onError) {
          return CustomErrorWidget(
              onPressed: () => {value.getNotifications()},
              errorMessage: value.errorMessage);
        } else if (value.notificationsUiState == UiState.onResult) {
          List<notif.Notification> data = value.notifications ?? [];
          return RefreshIndicator(
              onRefresh: () {
                return value.getNotifications();
              },
              child: ListView.builder(
                  itemCount: data.length,
                  itemBuilder: (context, index) {
                    notif.Notification notification = data[index];
                    return NotificationItem(
                      notification: notification,
                      onPressed: () {
                        if (notification.type == 0) {
                          List args = value.getProgress(
                              notification.activityId, notification.progressId);
                          Navigator.pushNamed(context, '/activity_approval',
                              arguments: args);
                        } else {
                          Navigator.pushNamed(context, '/add_progress',
                              arguments: [notification.activityId, false,["Q1", "Q2","Q3", "Q4"]]); // todo get from api
                        }
                      },
                    );
                  }));
        } else if (value.notificationsUiState == UiState.onLoading) {
          return Center(child: CircularProgressIndicator());
        } else {
          return Center(child: Text("Else branch"));
        }
      },
    );
  }
}
