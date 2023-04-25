import 'package:daf_project1/models/activity.dart';
import 'package:daf_project1/ui/widgets/custom_outlined_button.dart';
import 'package:daf_project1/utils/custom_styles.dart';
import 'package:daf_project1/utils/date_formatter.dart';
import 'package:expandable_text/expandable_text.dart';
import 'package:flutter/material.dart';
import 'circular_avators.dart';
import 'linear_progress_indicator.dart';

class ActivityItem extends StatelessWidget {
  final VoidCallback onPressed;
  final Activity activity;
  final bool isCoordinating;
  ActivityItem({this.isCoordinating = false, required this.activity, required this.onPressed, Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: EdgeInsets.symmetric(vertical: 4),
      child: GestureDetector(
        onTap: onPressed,
        child: Card(
          elevation: 2,
          child: Container(
            width: double.infinity,
            padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 16),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              mainAxisSize: MainAxisSize.min,
              children: [
              Row(
              crossAxisAlignment: CrossAxisAlignment.center,
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Text("Status: ${activity.status}",),
                    Text("${DateFormatter.formatDate(activity.shouldStart)} - ${DateFormatter.formatDate(activity.shouldEnd,)}",),]),
                Divider(thickness: 1,),
                Hero(
                    tag: activity.id,
                    child:
                    /*Text(activity.description,style: CustomStyles.activityTitle(context),)*/
                    ExpandableText(
                      activity.description,
                      linkStyle: Theme.of(context).textTheme.bodyText1
                      ,
                      style: CustomStyles.activityTitle(context),
                      expandText: 'More',
                      collapseText: 'Less',
                      maxLines: 2,
                      linkColor: Colors.blue,
                    ),
                ),
                SizedBox(height: 8,),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                 // direction: Axis.horizontal,
                  //alignment: WrapAlignment.spaceBetween,
                  children: [
                    Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text("Progress",style: Theme.of(context).textTheme.subtitle1,),
                        SizedBox(height: 4,),
                        Row(
                          mainAxisSize: MainAxisSize.min,
                          children: [
                            CustomLinearProgressIndicator(showPercent: false,percentile: activity.percentage),
                            SizedBox(width: 8,),
                            Text("${activity.percentage.toStringAsFixed(2)}", style: Theme.of(context).textTheme.subtitle1,),
                          ],
                        ),
                      ],
                    )
                    ,isCoordinating ? CustomOutlinedButton(onPressed: () => {
                      Navigator.pushNamed(context, "/report_history", arguments: activity)
                    }, buttonText: "Progresses",) : Container()
                  ],
                ),
                Divider(thickness: 1,),
                Row(
                  crossAxisAlignment: CrossAxisAlignment.center,
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    /*Text("Status: ${activity.status}",style: Theme.of(context).textTheme.caption,),
                    Text("${DateFormatter.formatDate(activity.shouldStart)} - ${DateFormatter.formatDate(activity.shouldEnd,)}",
                      style: Theme.of(context).textTheme.caption,),*/
                    Text("Assigned To"),
                    CustomAvatars(members: activity.assignedEmployees,)
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
