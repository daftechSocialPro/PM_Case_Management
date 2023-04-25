import 'package:daf_project1/models/activity.dart';
import 'package:daf_project1/ui/screens/activity_detail.dart';
import 'package:daf_project1/ui/screens/add_progress.dart';
import 'package:daf_project1/ui/screens/activity_approval.dart';
import 'package:daf_project1/ui/screens/coordinator_activity_detail.dart';
import 'package:daf_project1/ui/screens/full_screen_image.dart';
import 'package:daf_project1/ui/screens/home.dart';
import 'package:daf_project1/ui/screens/login.dart';
import 'package:daf_project1/ui/screens/notifications.dart';
import 'package:daf_project1/ui/screens/progress_history.dart';
import 'package:daf_project1/ui/screens/task_activities.dart';
import 'package:daf_project1/ui/screens/task_detail.dart';
import 'package:daf_project1/ui/screens/tasks.dart';
import 'package:daf_project1/ui/screens/terminate_activity.dart';
import 'package:flutter/material.dart';

const String initialRoute = "login";

class CustomRouter {
  static Route<dynamic> generateRoute(RouteSettings settings) {
    switch (settings.name) {
      case '/login':
        return MaterialPageRoute(builder: (_) => LoginPage());
      case '/home':
        return MaterialPageRoute(builder: (_) => HomePage());
      case '/tasks':
        return MaterialPageRoute(builder: (_)=>TasksPage());
      case '/activity_detail':
        var arg = settings.arguments as List;
        return MaterialPageRoute(builder: (_) => ActivityDetailPage(activityIndex: arg[1], taskIndex: arg[0],));
      case '/coordinator_activity_detail':
        var arg = settings.arguments as List;
        return MaterialPageRoute(builder: (_) => CoordinatorActivityDetailPage(isCoordinator: arg[2],activityIndex: arg[1], taskIndex: arg[0],));
      case '/activity_approval':
          var args = settings.arguments as List;
          return MaterialPageRoute(builder: (_)=> ActivityApproval(activity: args[0],
            progress: args[1],
          ));
      case '/task_activities':
          var args = settings.arguments as List;
          int taskIndex = args[0];
          bool isAssigned = args[1];
          return MaterialPageRoute(builder: (_)=> TaskActivitiesPage(isAssigned: isAssigned,taskIndex: taskIndex));
      case '/task_detail':
        var taskIndex = settings.arguments as int;
        return MaterialPageRoute(builder: (_)=> TaskDetail(taskIndex: taskIndex));
      case '/add_progress':
        var args = settings.arguments as List;

          return MaterialPageRoute(builder: (_)=> (AddProgressPage(isFinalize: args[1], activityId: args[0],dropDownItems: args[2],)));
      case '/notification':
          return MaterialPageRoute(builder: (_)=>NotificationPage());
      case '/report_history':
          var arg = settings.arguments as Activity;
          return MaterialPageRoute(builder: (_)=>ProgressHistory(activity: arg));
      case '/terminate_activity':
        var arg = settings.arguments as String;
          return  MaterialPageRoute(builder: (_)=>TerminateActivity(activityId: arg,));
      case '/full_screen_image':
        var arg = settings.arguments as String;
        return MaterialPageRoute(builder: (_)=>FullScreenPage(path: arg));
      default:
        return MaterialPageRoute(
            builder: (_) => Scaffold(
              body: Center(
                child: Text('No route defined for ${settings.name}'),
              ),
            ));
    }
  }
}

/**/