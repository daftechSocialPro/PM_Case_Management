
import 'package:daf_project1/models/notification.dart' as notif;
import 'package:daf_project1/utils/custom_styles.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

class NotificationItem extends StatelessWidget {
    final VoidCallback onPressed;
    final notif.Notification notification;
    NotificationItem({
    required this.notification,
    required this.onPressed, Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: EdgeInsets.symmetric(vertical: 2),
      child: GestureDetector(
        onTap: onPressed,
        child: Card(
          child: Padding(
            padding: const EdgeInsets.all(6),
            child: notification.type == 0 ? Column(
              children: [
                Text("Newly submitted progress is waiting for your approval."
                ,style: CustomStyles.activityTitle(context),),
                SizedBox(height: 8,),
                Text("${notification.activityName} under the task ${notification.taskName}.")
              ],
            ) :
            Text("Your progress submission is overdue.")
          ),
        ),
      ),
    );
  }

}
