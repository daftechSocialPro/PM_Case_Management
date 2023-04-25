import 'package:daf_project1/models/task.dart';
import 'package:daf_project1/ui/widgets/custom_circle_avatar.dart';
import 'package:daf_project1/ui/widgets/custom_divider.dart';
import 'package:daf_project1/utils/custom_styles.dart';
import 'package:daf_project1/utils/date_formatter.dart';
import 'package:expandable_text/expandable_text.dart';
import 'package:flutter/material.dart';
import 'package:focused_menu/focused_menu.dart';
import 'package:focused_menu/modals.dart';

import 'circular_avators.dart';
import 'custom_list_tile.dart';

class TaskItem extends StatelessWidget {
  final VoidCallback onPressed;
  final Task task;
  TaskItem({required this.task, required this.onPressed, Key? key})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: EdgeInsets.symmetric(vertical: 4),
      child: GestureDetector(
        onTap: this.onPressed,
        child: Card(
          elevation: 2,
          child: Padding(
            padding: const EdgeInsets.symmetric(vertical: 16.0),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 16.0),
                  /*child: Text(
                    task.description,
                    style: CustomStyles.taskTitle(context),
                  ),*/
                  child: ExpandableText(
                    task.description =="-----------"? task.planName:task.description,
                    linkStyle: Theme.of(context).textTheme.bodyText1
                    ,
                    style: CustomStyles.taskTitle(context),
                    expandText: 'More',
                    collapseText: 'Less',
                    maxLines: 2,
                    linkColor: Colors.blue,
                  ),
                ),
                SizedBox(height: 8,),
                Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 16.0),
                  child: Row(
                    crossAxisAlignment: CrossAxisAlignment.center,
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Text(
                        "${DateFormatter.formatDate(task.shouldStart)} - ${DateFormatter.formatDate(task.shouldEnd)}",
                      ),
                      Text("${task.activitiesLength} Activities"),
                    ],
                  ),
                ),
                CustomDivider(),
                Column(
                  children: [
                    CustomListTile(
                      leading: Icon(Icons.money),
                      title: Text("Planned Budget"),
                      trailing: Text("${task.plannedBudget} Birr"),
                    ),
                    CustomListTile(
                      leading: Icon(Icons.height),
                      title: Text("Weight"),
                      trailing: Text("${task.weight}"),
                    ),
                    CustomListTile(
                      leading: Icon(Icons.adjust_rounded),
                      title: Text("Goal"),
                      trailing: Text("${task.goal}"),
                    ),
                  ],
                ),
                CustomDivider(),
                Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 16.0),
                  child: Row(
                    crossAxisAlignment: CrossAxisAlignment.center,
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Text("${task.taskMembers.length} Members"),
                      CustomAvatars(members: task.taskMembers,),
                    ],
                  ),
                )
              ],
            ),
          ),
        ),
      ),
    );
  }
}
/*
 */