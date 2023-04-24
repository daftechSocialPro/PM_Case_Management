import 'package:daf_project1/models/coordinator_activity.dart';
import 'package:daf_project1/models/iactivity.dart';
import 'package:daf_project1/ui/widgets/custom_subtitle.dart';
import 'package:daf_project1/utils/custom_styles.dart';
import 'package:daf_project1/utils/date_formatter.dart';
import 'package:flutter/material.dart';

class ActivityItem extends StatelessWidget {
  VoidCallback onPressed;
  Activity activity;
  ActivityItem({required this.activity, required this.onPressed, Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: EdgeInsets.symmetric(vertical: 4),
      child: GestureDetector(
        onTap: onPressed,
        child: Card(
          child: Container(
            width: double.infinity,
            padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 16),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              mainAxisSize: MainAxisSize.min,
              children: [
                Text(activity.description,style: CustomStyles.activityTitle(context),),
                Divider(thickness: 1,),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    Text("Status: ${activity.status}",style: Theme.of(context).textTheme.caption,),
                    Text("${DateFormatter.formatDate(activity.shouldStart)} - ${DateFormatter.formatDate(activity.shouldEnd,)}",
                      style: Theme.of(context).textTheme.caption,),
                  ],
                )
              ],
            ),
          ),
        ),
      ),
    );
  }
}
