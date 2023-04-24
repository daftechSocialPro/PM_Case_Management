import 'package:daf_project1/models/notification.dart' as notif;
import 'package:daf_project1/ui/widgets/approval_notification_item.dart';
import 'package:daf_project1/viewmodels/notification_viewmodel.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';


class NotificationPage extends StatelessWidget {
  const NotificationPage({Key? key}) : super(key: key);


  @override
  Widget build(BuildContext context) {
    NotificationViewModel notificationViewModel = Provider.of<NotificationViewModel>(context, listen: false);
    return Scaffold(
      appBar: AppBar(
        title: Text("Notifications"),
      ),
      body: Container(
        child: _buildNotifications(context),
      )
    );
  }



  Widget _buildNotifications(BuildContext context) {
    NotificationViewModel notificationViewModel = Provider.of<NotificationViewModel>(context, listen: false);

    return FutureBuilder(
      builder: (context, projectSnap) {
        if (projectSnap.connectionState == ConnectionState.none &&
            projectSnap.hasData == null) {
          //print('project snapshot data is: ${projectSnap.data}');
          return Center(child: CircularProgressIndicator());
        }
        else if (projectSnap.data != null) {
          List<notif.Notification> data = projectSnap.data as List<notif.Notification>;
          return ListView.builder(
            itemCount: data.length,
            itemBuilder: (context, index) {
              notif.Notification notification = data[index];
              return ApprovalNotificationItem(
                  notification: notification,
                  onPressed:()=>Navigator.pushNamed(context, '/activity_approval'));
            },
          );
        }
        else {
          return Center(child: CircularProgressIndicator());
        }
      },
      future: notificationViewModel.getNotifications(),
    );
  }
}
