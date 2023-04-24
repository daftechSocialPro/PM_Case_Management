
import 'package:daf_project1/models/notification.dart' as notif;
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

class ApprovalNotificationItem extends StatelessWidget {
  VoidCallback onPressed;
  notif.Notification notification;
  ApprovalNotificationItem({
    required this.notification,
    required this.onPressed, Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: EdgeInsets.symmetric(vertical: 4),
      child: GestureDetector(
        onTap: onPressed,
        child: Card(
          child: Container(
            width: double.infinity,
            padding: const EdgeInsets.symmetric(horizontal: 8),
            child: ExpansionTile(title:Text(notification.title,  style: TextStyle(fontSize: 16.0, fontWeight: FontWeight.w500),),
            children: notification.description.map((e) => Column(
              children: [
                ListTile(title: Text(e.description)),
                Padding(
                  padding: const EdgeInsets.only(left: 16.0),
                  child: Divider(thickness: 0.5,),
                )
              ],
            )).toList(),)
          ),
        ),
      ),
    );
  }

  Widget _buildNotificationDescritpionItem(BuildContext context){
    return Container();

  }
}
