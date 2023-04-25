import 'dart:io';

import 'package:daf_project1/models/activity.dart';
import 'package:daf_project1/ui/widgets/add_file_button.dart';
import 'package:daf_project1/ui/widgets/custom_input_field.dart';
import 'package:daf_project1/ui/widgets/dropdown_menu.dart';
import 'package:daf_project1/ui/widgets/primary_button.dart';
import 'package:daf_project1/viewmodels/add_progress_viewmodel.dart';
import 'package:daf_project1/viewmodels/ui_state.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/scheduler.dart';
import 'package:provider/provider.dart';

class AddProgressPage extends StatefulWidget {
  final bool isFinalize;
  final String activityId;
  final List<TargetDivision> dropDownItems;

  AddProgressPage(
      {required this.dropDownItems,
        required this.activityId,
        required this.isFinalize,
        Key? key})
      : super(key: key);

  @override
  _AddProgressPageState createState() => _AddProgressPageState();
}

class _AddProgressPageState extends State<AddProgressPage> {
  String currentValue = "Select a quarter";
  @override
  Widget build(BuildContext context) {
    AddProgressViewModel addProgressViewModel =
    Provider.of<AddProgressViewModel>(context, listen: false);
    return Scaffold(
        resizeToAvoidBottomInset: false,
        appBar: AppBar(
          title: Text(widget.isFinalize ? "Finalize" : "AddProgress"),
        ),
        body: Container(
            padding: EdgeInsets.only(bottom: 16, left: 16, right: 16, top: 16),
            child: Consumer<AddProgressViewModel>(
              builder: (context, value, _) {
                if (value.addProgressUiState == UiState.onLoading) {
                  return Center(
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.center,
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        CircularProgressIndicator(),
                        SizedBox(
                          height: 16,
                        ),
                        Text("Uploading your progress...")
                      ],
                    ),
                  );
                } else {
                  if (value.addProgressUiState == UiState.onError) {
                    SchedulerBinding.instance!.addPostFrameCallback((_) {
                      final snackBar = SnackBar(
                          content: Text(value.errorMessage));
                      ScaffoldMessenger.of(context).showSnackBar(snackBar).closed
                          .then((value) => ScaffoldMessenger.of(context).clearSnackBars());
                    });
                    value.resetAddProgressUiState();
                  }
                  else if(value.addProgressUiState == UiState.onResult){
                    value.resetAddProgressUiState();
                    SchedulerBinding.instance!.addPostFrameCallback((_) {
                      final snackBar = SnackBar(
                          content: Text('Your progress is uploaded successfully...'));
                      ScaffoldMessenger.of(context)
                          .showSnackBar(snackBar).closed
                          .then((value) => ScaffoldMessenger.of(context).clearSnackBars());
                    });
                  }
                  return Column(
                    mainAxisSize: MainAxisSize.max,
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      DropDown(
                        onItemPressed: (value)=>{
                          setState(() {
                            currentValue = value;
                            addProgressViewModel.setQuarterId(widget.dropDownItems.firstWhere((element) => element.target == value).id);
                          })
                        },
                        dropDownItems: widget.dropDownItems.map((e) => e.target).toList(),
                        label: currentValue,
                      ),
                      CustomInputField(
                        label: "Progress made",
                        controller:
                        addProgressViewModel.progressFieldController,
                        errorMsg: "",
                        isNumber: true,
                      ),
                      CustomInputField(
                        label: "Budget used",
                        controller: addProgressViewModel.budgetFieldController,
                        errorMsg: "",
                        isNumber: true,
                      ),
                      CustomInputField(
                        label: "Remark",
                        controller: addProgressViewModel.remarkFieldController,
                        errorMsg: "",
                        isMultiLine: true,
                      ),
                      Expanded(
                        child: Consumer<AddProgressViewModel>(
                            builder: (context, value, _) {
                              String selectedFileName = value.selectedFileName;
                              bool isFileSelected = selectedFileName != "";
                              List<String> selectedFilesName =
                                  value.selectedFilesName;
                              bool isFilesSelected = selectedFilesName != [];
                              return ListView(
                                children: [
                                  Row(
                                    mainAxisAlignment:
                                    MainAxisAlignment.spaceBetween,
                                    children: [
                                      Flexible(
                                        fit: FlexFit.loose,
                                        child: AddFileButton(
                                          showIcon: !isFilesSelected,
                                          label: isFilesSelected
                                              ? "${selectedFilesName.length} Files selected"
                                              : "Add Doc",
                                          onPressed: () => {value.addFile(true)},
                                        ),
                                      ),
                                      Flexible(
                                        fit: FlexFit.loose,
                                        child: AddFileButton(
                                          showIcon: !isFileSelected,
                                          label: !isFileSelected
                                              ? "Add Finance Doc"
                                              : selectedFileName,
                                          onPressed: () => {value.addFile(false)},
                                        ),
                                      )
                                    ],
                                  ),
                                  isFilesSelected
                                      ? Column(
                                      mainAxisSize: MainAxisSize.min,
                                      children: List<Widget>.from(value
                                          .selectedFilesName
                                          .map((e) => ListTile(
                                        title: Text(e),
                                      ))))
                                      : Container()
                                ],
                              );
                            }),
                      ),
                      Align(
                        alignment: AlignmentDirectional.bottomCenter,
                        child: PrimaryButton(
                          buttonState: ButtonState.idle,
                          onPressed: () async {
                            print("Adding Progress to the server");
                            await addProgressViewModel.addProgress(widget.activityId,
                                isFinalize: widget.isFinalize);
                          },
                          buttonText: widget.isFinalize ? "Finalize" : "Add",
                        ),
                      )
                    ],
                  );
                }
              },
            )));
  }
  List<Widget> buildFileNameItem(List<String> names) {
    if (names == []) return [Container()];
    return names
        .map((e) => ListTile(
      title: Text(e),
    ))
        .toList();
  }
}
