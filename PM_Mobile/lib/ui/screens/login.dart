import 'package:daf_project1/services/api.dart';
import 'package:daf_project1/services/shared_preferences.dart';
import 'package:daf_project1/ui/screens/home.dart';
import 'package:daf_project1/ui/screens/settings.dart';
import 'package:daf_project1/ui/widgets/custom_input_field.dart';
import 'package:daf_project1/ui/widgets/primary_button.dart';
import 'package:daf_project1/viewmodels/login_viewmodel.dart';
import 'package:daf_project1/viewmodels/settings_viewmodel.dart';
import 'package:daf_project1/viewmodels/ui_state.dart';
import 'package:daf_project1/viewmodels/user_auth.dart';
import 'package:flutter/material.dart';
import 'package:flutter/scheduler.dart';
import 'package:provider/provider.dart';

import '../../locator.dart';

class LoginPage extends StatelessWidget {
   LoginPage({Key? key}) : super(key: key);


  @override
  Widget build(BuildContext context) {
    print("LoginPage Build is called");
    LoginViewModel loginViewModel = Provider.of<LoginViewModel>(context,listen: false);
    return Scaffold(
       resizeToAvoidBottomInset: true,
        appBar: AppBar(
          title: Text("Login"),
          backgroundColor: Colors.blue[300],
        ),
        body: SingleChildScrollView(
  child: Padding(
    padding: const EdgeInsets.all(16.0),
    child: SingleChildScrollView(
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.end,
        children: [
          IconButton(
            icon: Icon(Icons.settings),
            onPressed: (){
              showModalBottomSheet(
                context: context,
                isScrollControlled: true,
                builder:(context){
                  return Padding(
                    padding: EdgeInsets.only(bottom: MediaQuery.of(context).viewInsets.bottom),
                    child: ChangeNotifierProvider<SettingsViewModel>(
                      create: (_)=>SettingsViewModel(locator<SharedPreferencesUtil>()),
                      child: SettingsPage()
                    )
                  );
                },
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.only(
                    topLeft: Radius.circular(10.0),
                    topRight: Radius.circular(10.0)
                  )
                )
              );
            },
          ),
          Container(
            padding: const EdgeInsets.symmetric(horizontal: 32.0, vertical: 16.0),
            child: Consumer<LoginViewModel>(
              builder: (context,value,_){
                if(value.loginUiState == UiState.onError){
                  print("OnError block inside login screen");
                  SchedulerBinding.instance!.addPostFrameCallback((_) {
                    ScaffoldMessenger.of(context).showSnackBar(SnackBar(
                      backgroundColor: Theme.of(context).errorColor,
                      content: Text(value.errorMessage))
                    ).closed.then((value) => ScaffoldMessenger.of(context).clearSnackBars());
                  });
                  value.resetLoginUiState();
                }
                else if(value.loginUiState == UiState.onResult){
                  SchedulerBinding.instance!.addPostFrameCallback((_) {
                    final UserAuthentication userAuth = Provider.of<UserAuthentication>(context, listen: false);
                    userAuth.isAuthenticated = true;
                    /*Navigator.of(context)
                      .pushNamedAndRemoveUntil('/home',(Route<dynamic> route) => false);*/
                  });
                  loginViewModel.resetLoginUiState();
                }
                return Column(
                  mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                  children: [
                    Image.asset('assets/logo.png', height: 180,),
                    CustomInputField(
                      focusOnNext: true,
                      label: "Username",
                      errorMsg: !value.isUsernameValid? "Username can't be empty" : '',
                      controller: loginViewModel.usernameController
                    ),
                    CustomInputField(
                      label: "Password",
                      obscureText: true,
                      errorMsg: !value.isPasswordValid ? "Password is invalid" : '',
                      controller: loginViewModel.passwordController
                    ),
                    SizedBox(height: 16),
                    PrimaryButton(
                      onPressed: value.isValid() ? (){
                        value.login();
                      } : null,
                      buttonText: 'Login',
                      buttonState: value.loginUiState == UiState.onLoading ? ButtonState.loading : ButtonState.idle,
                    )
                  ],
                );
              }
            ),
          ),
        ],
      ),
    ),
  ),
));

  }

}
