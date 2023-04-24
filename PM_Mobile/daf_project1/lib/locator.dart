
import 'package:daf_project1/services/api.dart';
import 'package:daf_project1/services/file_picker.dart';
import 'package:daf_project1/services/shared_preferences.dart';
import 'package:daf_project1/viewmodels/login_viewmodel.dart';
import 'package:daf_project1/viewmodels/tasks_viewmodel.dart';
import 'package:file_picker/file_picker.dart';
import 'package:get_it/get_it.dart';
import 'package:shared_preferences/shared_preferences.dart';

GetIt locator = GetIt.instance;

void setupLocator() {
  /*locator.registerLazySingleton<SharedPreferences>(({
      //create sharedpreferences
  });*/
  locator.registerLazySingleton<FilePickerUtil>(() => FilePickerUtil());
  locator.registerSingleton<SharedPreferencesUtil>(SharedPreferencesUtil());
  locator.registerLazySingleton<Api>(() => Api());
  locator.registerLazySingleton(() => LoginViewModel(locator<Api>()));
}
