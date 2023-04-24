
import 'package:daf_project1/locator.dart';
import 'package:daf_project1/models/coordinator_activity.dart';
import 'package:daf_project1/services/api.dart';
import 'package:daf_project1/viewmodels/activities_viewmodel.dart';
import 'package:daf_project1/viewmodels/notification_viewmodel.dart';
import 'package:daf_project1/viewmodels/tasks_viewmodel.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import 'ui/router.dart';

void main() async{
  WidgetsFlutterBinding.ensureInitialized();
  setupLocator();

  //remove after mockup
  /*var api = locator<Api>();
  var t = await api.getTasks();*/

  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  Api _api = locator<Api>();
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MultiProvider(
      providers: [
        ChangeNotifierProvider<TasksViewModel>(create:(context)=> TasksViewModel(_api)),
        ChangeNotifierProvider<ActivitiesViewModel>(create:(context)=> ActivitiesViewModel(_api)),
        ChangeNotifierProvider<NotificationViewModel>(create:(context)=> NotificationViewModel(_api)),
      ],
      child: MaterialApp(
        title: 'Flutter Demo',
        theme: ThemeData(
          primarySwatch: Colors.blue,
        ),
        initialRoute: '/login',//todo decide the next page based on user auth status (if logged in or not)
        onGenerateRoute: CustomRouter.generateRoute,
      ),
    );
  }
}

/*todo
  1, Drawer (
        Img->FullName -> ( MembershipLevel | Structure)
        three buttons( Coordinating, working on, notifications)
        signout
    )
  2, LoginPage
    Logo
    Integrate result of sharedPreference
  3, Test Api Login() function
  4, add Lat, long of user's current location on addProgress
  (required Parm: actId, actualBudget(budget Used), quarterId, remark, progressStats =>(0: onProgress, 1, Finalized)
* */
