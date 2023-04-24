import 'dart:io';

import 'package:daf_project1/locator.dart';
import 'package:daf_project1/services/file_picker.dart';
import 'package:daf_project1/ui/widgets/add_file_button.dart';
import 'package:daf_project1/ui/widgets/custom_input_field.dart';
import 'package:daf_project1/ui/widgets/dropdown_menu.dart';
import 'package:daf_project1/ui/widgets/primary_button.dart';
import 'package:daf_project1/viewmodels/login_viewmodel.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class AddProgressPage extends StatelessWidget {
  bool isFinalize;
  AddProgressPage({ this.isFinalize = false, Key? key}) : super(key: key);


  @override
  Widget build(BuildContext context) {
    return Scaffold(
        resizeToAvoidBottomInset: false,
        appBar: AppBar(
          title: Text(isFinalize ? "Finalize" : "AddProgress"),
        ),
        body: Container(
          padding: EdgeInsets.only(bottom: 16, left: 16, right: 16, top: 16),
          child: Column(
            mainAxisSize: MainAxisSize.max,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              DropDownDemo(),
              CustomInputField(
                label: "Progress made",
                controller: TextEditingController(),
                errorMsg: "",
                isNumber: true,
              ),
              CustomInputField(
                label: "Budget used",
                controller: TextEditingController(),
                errorMsg: "",
                isNumber: true,
              ),
              CustomInputField(
                label: "Remark",
                controller: TextEditingController(),
                errorMsg: "",
                isMultiLine: true,
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                AddFileButton(label: "Add Doc", selectMultipleFiles: true),
                AddFileButton(label: "Add Finance Doc", selectMultipleFiles: false)
              ],),
              Expanded(
                  child: Align(
                alignment: AlignmentDirectional.bottomCenter,
                child: PrimaryButton(
                  buttonState: ButtonState.idle,
                  onPressed: () {},
                  buttonText: isFinalize ? "Finalize" : "Add",
                ),
              ))
            ],
          ),
        ));
  }

}
