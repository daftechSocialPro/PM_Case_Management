

import 'package:daf_project1/models/activity.dart';
import 'package:daf_project1/models/activity_progress.dart';
import 'package:daf_project1/ui/widgets/custom_outlined_button.dart';
import 'package:daf_project1/ui/widgets/custom_subtitle.dart';
import 'package:daf_project1/ui/widgets/download_file_item.dart';
import 'package:daf_project1/ui/widgets/linear_progress_indicator.dart';
import 'package:daf_project1/ui/widgets/primary_button.dart';
import 'package:daf_project1/ui/widgets/progress_indicator.dart';
import 'package:daf_project1/viewmodels/activity_approval_viewmodel.dart';
import 'package:daf_project1/viewmodels/ui_state.dart';
import 'package:flutter/material.dart';
import 'package:flutter_cached_pdfview/flutter_cached_pdfview.dart';
import 'package:provider/provider.dart';

import 'coordinating_activities.dart';
import 'home.dart';

class ActivityApproval extends StatelessWidget {
  ActivityApproval({required this.activity, required this.progress, Key? key})
      : super(key: key);
  final Activity activity;

final ButtonStyle raisedButtonStyle = ElevatedButton.styleFrom(
  onPrimary: Colors.black87,
  primary: Colors.blue[300],
  
  
  padding: EdgeInsets.symmetric(horizontal: 16),
  shape: const RoundedRectangleBorder(
    borderRadius: BorderRadius.all(Radius.circular(2)),
  ),
);

final ButtonStyle raisedButtonStyle2 = ElevatedButton.styleFrom(
  onPrimary: Colors.black87,
  primary: Colors.red[300],
  minimumSize: Size(88, 36),
  padding: EdgeInsets.symmetric(horizontal: 16),
  shape: const RoundedRectangleBorder(
    borderRadius: BorderRadius.all(Radius.circular(2)),
  ),
);


  final ActivityProgress progress;
  late String remark="";
  late String cordinatorRemark = "";
  late String financeRemark = "";
  @override
  Widget build(BuildContext context) {
    ActivityApprovalViewModel activityApprovalViewModel =
    Provider.of<ActivityApprovalViewModel>(context, listen: false);
    activityApprovalViewModel.init(activity: activity, progress: progress);
    return Scaffold(
        appBar: AppBar(
          title: Text("Approval"),
        ),
        body: Container(
          padding: EdgeInsets.symmetric(vertical: 20),
          child:
          Consumer<ActivityApprovalViewModel>(builder: (context, value, _) {
            return Card(
              margin: EdgeInsets.all(10),
              
            
              child: Padding(
                padding: EdgeInsets.fromLTRB(10, 20, 10, 20),
                child: Column(
            
              children: [
                Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 16.0),
                

                  child: Row(
                      mainAxisAlignment: MainAxisAlignment.spaceBetween,
                      children: [
                        SubtitleText(
                          "Progress So far",
                        ),
                        CustomLinearProgressIndicator(
                            percentile: value.activity.percentage)
                      ]),
                ),
                ListTile(
                  trailing: Text(value.activity.actualBudget.toString()),
                  title: Text("Budget used so far:"),
                ),
               
                ListTile(
                  trailing: Text(value.progress.actualWorked.toString()),
                  title: Text("Current Progress:"),
                ),
                ListTile(
                  trailing: Text(value.progress.actualBudget.toString()),
                  title: Text("Current Budget used:"),
                ),
                ListTile(
                  title: SubtitleText("Remark"),
                  subtitle: Text(value.progress.remark),
                ),
                 ListTile(
                  title: SubtitleText("Director Remark"),
                  
                ),
                Padding(
                    padding: const EdgeInsets.symmetric(
                        vertical: 8.0, horizontal: 16),
                    child: ListView(
                      shrinkWrap: true,
                      children: [
                        DownloadItem(
                            isFileExist: true,
                            onPressed: ()
                            {
                              String extension = value.progress.attachments.first
                                  .filePath
                                  .split('.')
                                  .last;
                              extension = extension.toLowerCase();
                              String filePath = value.getRelativeFilePath() + value.progress.attachments.first.filePath;
                              print("File Path: $filePath");
                              print("Extension: $extension");
                              if(extension == "pdf"){
                                openFileReader(value.progress.attachments.first.filePath, context);

                              }
                              else if(extension == "jpg" || extension == "png" || extension == "jpeg"){
                                Navigator.pushNamed(context, '/full_screen_image',
                                    arguments:filePath);
                              }
                              else{
                                print("Invalid file format is used...");
                              }
                            },
                            label: "Docs Attached",
                            fileName:
                            "Attached File 1.${value.progress.attachments.first
                                .filePath
                                .split('.')
                                .last}"),
                        ...(value.progress.attachments
                            .getRange(1, value.progress.attachments.length)
                            .map(
                              (e) =>
                              DownloadItem(
                                  isFileExist: true,
                                  onPressed: () {
                                    String extension =e.filePath.split('.').last;
                                    extension = extension.toLowerCase();
                                    String filePath = value.getRelativeFilePath() + e.filePath;
                                    print("File path $filePath");
                                    print("Extension: $extension");
                                    if(extension == "pdf"){
                                      openFileReader(e.filePath, context);

                                    }
                                    else if(extension == "jpg" || extension == "png" || extension == "jpeg"){
                                      Navigator.pushNamed(context, '/full_screen_image',
                                          arguments:filePath);
                                    }
                                    else{
                                      print("Invalid file format is used...");
                                    }
                                  //value.downloadFile(e.filePath,"Attached File ${value.progress.attachments.indexOf(e) + 1}")
                                  },
                                  label: "",
                                  fileName: "Attached File ${value.progress
                                      .attachments.indexOf(e) + 1}.${e.filePath
                                      .split('.')
                                      .last}"),
                        )
                            .toList()),
                        Visibility(
                          visible: value.progress.financeDocPath != null,
                          child: DownloadItem(
                              isFileExist: true,
                              onPressed: ()
                              {
                              String extension =value.progress.financeDocPath!.split('.').last;
                              extension = extension.toLowerCase();
                              String? filePath = value.getRelativeFilePath() + (value.progress.financeDocPath?? "");
                              print("File path $filePath");
                              print("Extension: $extension");
                              if(extension == "pdf"){
                                   openFileReader(value.progress.financeDocPath, context);
                              }
                              else if(extension == "jpg" || extension == "png" || extension == "jpeg"){
                                  Navigator.pushNamed(context, '/full_screen_image',
                                   arguments:filePath);
                              }
                              else{
                                print("Invalid file format is used...");
                              }
                              },
                              label: "Finance File Attached",
                              fileName: "Finance file.${value.progress
                                  .financeDocPath!.split('.').last}"),
                        ),
                      ],
                    )),
                  Card(
                    
                    child: Padding(
                      padding: EdgeInsets.fromLTRB(10, 20, 10, 10),
                      child: Column(
                        children:[  
                         
                Visibility(
                  visible: value.isEmployeeFinance(),
                  child: Column(
                    children: [
                      buildCustomSwitch(
                      "Finance",
                      value.progress.isApprovedByFinance==1?0:1,
                      context,
                          (currentValue) =>
                      {
                       
                      }),
                      TextField(
                            minLines: 3,
                            maxLines:null,
                            decoration: InputDecoration(
                              border: OutlineInputBorder(),
                              labelText: 'Remark'),
                            onChanged: (text) {this.financeRemark=text;print(this.financeRemark);},

                        ),

                                                Row(
  children: <Widget>[

    

SizedBox(
  width: 100,
  child:  Expanded(
      child: ElevatedButton (
        style: raisedButtonStyle,
        child: Text('Approve'),
        onPressed: ()  {
       
      showDialog(
      context: context, barrierDismissible: false, // user must tap button!

      builder: (BuildContext context) {
        return new AlertDialog(
          title: new Text('ApprovaL Status'),
          content: new SingleChildScrollView(
            child: new ListBody(
              children: [
                new Text('You have successfully changed the Progress Status'),
              ],
            ),
          ),
          actions: [
            new TextButton(
              child: new Text('Ok'),
              onPressed: () {
         

                Navigator.of(context).pop();
                  Navigator.push(
    context,
    MaterialPageRoute(
    	builder: (context) => HomePage()),
  );
              },
              
            ),
          ],
        );
      },
    );
       value.setApprovalStatus(ApprovalStatus.finance, true,this.financeRemark.isEmpty?"":this.financeRemark);
                       
        }),
    ),
   
),
    
    SizedBox(
      width: 100,
      child:    Expanded(
      child: ElevatedButton (
        style: raisedButtonStyle2,
        child: Text('Reject'),
        onPressed: (){
  showDialog(
      context: context, barrierDismissible: false, // user must tap button!

      builder: (BuildContext context) {
        return new AlertDialog(
          title: new Text('ApprovaL Status'),
          content: new SingleChildScrollView(
            child: new ListBody(
              children: [
                new Text('You have successfully changed the Progress Status'),
              ],
            ),
          ),
          actions: [
            new TextButton(
              child: new Text('Ok'),
              onPressed: () {
         

                Navigator.of(context).pop();
                  Navigator.push(
    context,
    MaterialPageRoute(
    	builder: (context) => HomePage()),
  );
              },
              
            ),
          ],
        );
      },
    );
    value.setApprovalStatus(ApprovalStatus.finance, false,this.financeRemark.isEmpty?"":this.financeRemark);
       
        } ,
      ),
    )
  ,
    )
  ],
),
                    ],
                  )
                ),
                Visibility(
                  visible: value.isEmployeeCoordinator(),
                  child: Column(
                    children: [
                       buildCustomSwitch(
                      "Coordinator",
                      value.progress.isApprovedByCoordinator==1?0:1,
                      context,
                          (currentValue) =>
                      {
                        
                      }),
                       TextField(
                            minLines: 3,
                            maxLines:null,
                            decoration: InputDecoration(
                              border: OutlineInputBorder(),
                              labelText: 'Remark'),
                            onChanged: (text) {this.cordinatorRemark=text;print(this.cordinatorRemark);},

                        ),
                        Row(
  children: <Widget>[

    

SizedBox(
  width: 100,
  child:  Expanded(
      child: ElevatedButton (
        style: raisedButtonStyle,
        child: Text('Approve'),
        onPressed: ()  {
       
      showDialog(
      context: context, barrierDismissible: false, // user must tap button!

      builder: (BuildContext context) {
        return new AlertDialog(
          title: new Text('ApprovaL Status'),
          content: new SingleChildScrollView(
            child: new ListBody(
              children: [
                new Text('You have successfully changed the Progress Status'),
              ],
            ),
          ),
          actions: [
            new TextButton(
              child: new Text('Ok'),
              onPressed: () {
         

                Navigator.of(context).pop();
                  Navigator.push(
    context,
    MaterialPageRoute(
    	builder: (context) => HomePage()),
  );
              },
              
            ),
          ],
        );
      },
    );
       value.setApprovalStatus(
                            ApprovalStatus.coordinator, true,this.cordinatorRemark.isEmpty ?"":this.cordinatorRemark);
                       
        }),
    ),
   
),
    
    SizedBox(
      width: 100,
      child:    Expanded(
      child: ElevatedButton (
        style: raisedButtonStyle2,
        child: Text('Reject'),
        onPressed: (){
  showDialog(
      context: context, barrierDismissible: false, // user must tap button!

      builder: (BuildContext context) {
        return new AlertDialog(
          title: new Text('ApprovaL Status'),
          content: new SingleChildScrollView(
            child: new ListBody(
              children: [
                new Text('You have successfully changed the Progress Status'),
              ],
            ),
          ),
          actions: [
            new TextButton(
              child: new Text('Ok'),
              onPressed: () {
         

                Navigator.of(context).pop();
                  Navigator.push(
    context,
    MaterialPageRoute(
    	builder: (context) => HomePage()),
  );
              },
              
            ),
          ],
        );
      },
    );
    value.setApprovalStatus(
                            ApprovalStatus.coordinator, false,this.cordinatorRemark.isEmpty ?"":this.cordinatorRemark);
       
        } ,
      ),
    )
  ,
    )
  ],
),

                    ],
                  ),
                ),
                Visibility(
                  visible: value.isEmployeeDirector(),

                  child:Column(children: [
  
buildCustomSwitch(
     
                    
                      "Director",
                      value.progress.isApprovedByDirector==1?0:1,
                      context,
                          (currentValue) =>
                      {
                        value.setApprovalStatus(
                            ApprovalStatus.director, currentValue,this.remark)
                      }),
                       TextField(
                            minLines: 3,
                            maxLines:null,
                            decoration: InputDecoration(
                              border: OutlineInputBorder(),
                              labelText: 'Remark'
                      ),
                      onChanged: (text) {
  this.remark=text;
  print(this.remark);
  },

                    ),
                                        Row(
  children: <Widget>[

    

SizedBox(
  width: 100,
  child:  Expanded(
      child: ElevatedButton (
        style: raisedButtonStyle,
        child: Text('Approve'),
        onPressed: ()  {
       
      showDialog(
      context: context, barrierDismissible: false, // user must tap button!

      builder: (BuildContext context) {
        return new AlertDialog(
          title: new Text('ApprovaL Status'),
          content: new SingleChildScrollView(
            child: new ListBody(
              children: [
                new Text('You have successfully changed the Progress Status'),
              ],
            ),
          ),
          actions: [
            new TextButton(
              child: new Text('Ok'),
              onPressed: () {
         

                Navigator.of(context).pop();
                  Navigator.push(
    context,
    MaterialPageRoute(
    	builder: (context) => HomePage()),
  );
              },
              
            ),
          ],
        );
      },
    );
    value.setApprovalStatus(ApprovalStatus.director, true,this.remark);
        
        }),
    ),
   
),
    
    SizedBox(
      width: 100,
      child:    Expanded(
      child: ElevatedButton (
        style: raisedButtonStyle2,
        child: Text('Reject'),
        onPressed: (){
  showDialog(
      context: context, barrierDismissible: false, // user must tap button!

      builder: (BuildContext context) {
        return new AlertDialog(
          title: new Text('ApprovaL Status'),
          content: new SingleChildScrollView(
            child: new ListBody(
              children: [
                new Text('You have successfully changed the Progress Status'),
              ],
            ),
          ),
          actions: [
            new TextButton(
              child: new Text('Ok'),
              onPressed: () {
         

                Navigator.of(context).pop();
                  Navigator.push(
    context,
    MaterialPageRoute(
    	builder: (context) => HomePage()),
  );
              },
              
            ),
          ],
        );
      },
    );
    value.setApprovalStatus(ApprovalStatus.director, false,this.remark);
        
        } ,
      ),
    )
  ,
    )
  ],
),
                  ],) 
                      



                ),]
                      ) ,
                    ),
                  )
              ],
            ),
              )
            );
          }),
        ));
  }

  Widget buildCustomSwitch(String title, int value, BuildContext context,
      Function onPressed) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 16.0),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          Text(
            "$title",
            style: Theme
                .of(context)
                .textTheme
                .button,
          ),
          SizedBox(
            width: 8,
          ),
          Switch(value: value==0, onChanged: (value) => {onPressed(value)}),
        ],
      ),
    );
  }

  openFileReader(String? filePath, BuildContext context){
    if(filePath != null) {
      String fullPath = Provider.of<ActivityApprovalViewModel>(context, listen:  false).getRelativeFilePath() +  filePath;
      Navigator.push(
        context,
        MaterialPageRoute<dynamic>(
          builder: (_) =>
              PDFViewerCachedFromUrl(
                  url: '$fullPath'
              ),
        ),);
    }

  }
}
class ImageViewer extends StatelessWidget{
  ImageViewer({required this.imagePath});
  final String imagePath;
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('File Reader'),
      ),
      body: Image.network(imagePath),
    );
  }



}
class PDFViewerCachedFromUrl extends StatelessWidget {
  const PDFViewerCachedFromUrl({Key? key, required this.url}) : super(key: key);

  final String url;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('File Reader'),
      ),
      body: const PDF().cachedFromUrl(
        url,
        placeholder: (double progress) => Center(child: Text('$progress %')),
        errorWidget: (dynamic error) => Center(child: Text(error.toString())),
      ),
    );
  }


 

  



}
