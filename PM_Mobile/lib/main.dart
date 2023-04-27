import 'package:daf_project1/locator.dart';
import 'package:daf_project1/services/api.dart';
import 'package:daf_project1/services/file_picker.dart';
import 'package:daf_project1/services/mac_getter.dart';
import 'package:daf_project1/ui/screens/home.dart';
import 'package:daf_project1/ui/screens/login.dart';
import 'package:daf_project1/viewmodels/activities_viewmodel.dart';
import 'package:daf_project1/viewmodels/activity_approval_viewmodel.dart';
import 'package:daf_project1/viewmodels/add_progress_viewmodel.dart';
import 'package:daf_project1/viewmodels/home_viewmodel.dart';
import 'package:daf_project1/viewmodels/login_viewmodel.dart';
import 'package:daf_project1/viewmodels/notification_viewmodel.dart';
import 'package:daf_project1/viewmodels/request_termination_viewmodel.dart';
import 'package:daf_project1/viewmodels/task_detail_viewmodel.dart';
import 'package:daf_project1/viewmodels/tasks_viewmodel.dart';
import 'package:daf_project1/viewmodels/user_auth.dart';
import 'package:flutter/material.dart';
import 'package:flutter_downloader/flutter_downloader.dart';
import 'package:provider/provider.dart';

import 'ui/router.dart';

void main() async{

  WidgetsFlutterBinding.ensureInitialized();
  setupLocator();
  await FlutterDownloader.initialize();
  await locator<MacGetter>().initMacAddress();

final Color? lovelyBlue = Colors.blue[300];
  //remove after mockup
  /*var api = locator<Api>();
  var t = await api.getTasks();*/
  runApp(
    MultiProvider(
      providers: [
        ChangeNotifierProvider<UserAuthentication>(
          create: (final BuildContext context) {
            return UserAuthentication();
          },
        )
      ],
      child: MyApp(),
    ),
  );
}

class MyApp extends StatelessWidget {
  Api _api = locator<Api>();
  FilePickerUtil _filePicker = locator<FilePickerUtil>();
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return  Consumer<UserAuthentication>(
        builder: (context,value,_) {
          return MultiProvider(
              key: ObjectKey(_api.user),
              providers: [
                ChangeNotifierProvider<HomeViewModel>(
                    create: (context) => HomeViewModel(_api)),
                ChangeNotifierProvider<TasksViewModel>(
                    create: (context) => TasksViewModel(_api)),
                ChangeNotifierProvider<TaskDetailViewModel>(
                    create: (context) => TaskDetailViewModel(_api)),
                ChangeNotifierProvider<ActivitiesViewModel>(
                    create: (context) => ActivitiesViewModel(_api)),
                ChangeNotifierProvider<NotificationViewModel>(
                    create: (context) => NotificationViewModel(_api)),
                ChangeNotifierProvider<LoginViewModel>(
                    create: (context) => LoginViewModel(_api)),
                ChangeNotifierProvider<AddProgressViewModel>(
                    create: (context) => AddProgressViewModel(_api,_filePicker )),
                ChangeNotifierProvider<RequestTerminationViewModel>(
                    create: (context) => RequestTerminationViewModel(_api,_filePicker )),
                ChangeNotifierProvider<ActivityApprovalViewModel>(
                    create: (context) => ActivityApprovalViewModel(_api)),

              ],
              child: MaterialApp(
                key: GlobalObjectKey(context),
                title: 'Project Managment',
                theme: ThemeData(
                  primarySwatch : Colors.blue,
                ),
                home: value.isAuthenticated ? HomePage() : LoginPage(),
                //initialRoute: isSignedIn ? '/home' : '/login',
                onGenerateRoute: CustomRouter.generateRoute,
              )
          );
        },
    );
  }
}
/*\
* Main Todo that is left to be implemented in this application :
*
*   1, If possible, get the mac address of the mobile phone the user in
*   2, If not , just get some kind of a unique id so that you can display in the settings
* page and show the user to let the admin reg the phone in the database
*
*
* */