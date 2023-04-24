import 'dart:math';

import 'package:daf_project1/models/coordinator_activity.dart';
import 'package:daf_project1/models/task.dart';
import 'package:daf_project1/services/api.dart';
import 'package:daf_project1/viewmodels/login_viewmodel.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../locator.dart';

class TasksViewModel extends ChangeNotifier{

      late Api _api;
      late List<Task> tasks;

      TasksViewModel(Api api){
            _api = api;
            _api.getTasks().then((value) => tasks = value);
      }

      Future<List<Task>> getTasks() async{
            return _api.getTasks().then((value) => tasks = value);
      }
}