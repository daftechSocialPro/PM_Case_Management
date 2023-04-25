import 'dart:async';
import 'dart:convert';
import 'dart:io';
import 'package:daf_project1/models/new_progress.dart';
import 'package:daf_project1/services/shared_preferences.dart';
import 'package:http/http.dart' as http;
import '../locator.dart';
import 'app_exception.dart';

class ApiBaseHelper {
  late String _baseUrl;

  ApiBaseHelper() {
    _baseUrl = buildUrl();
  }

  String buildUrl() {
    // Sample url
    //http://192.168.0.112/IPDCS%20MObile/mobileuser/login?UserName=besha&Password=P@ssw0rd#
    SharedPreferencesUtil preferencesUtil = locator<SharedPreferencesUtil>();
    String _ipAddress = preferencesUtil.getIpAddress();
    int _port = preferencesUtil.getPortNumber();
    String _publishName = preferencesUtil.getPublishName();
    print("IpAddress: $_ipAddress");
    return "http://$_ipAddress:$_port/$_publishName/mobileuser/";
  }

  String buildRelativeImagePath() {
    SharedPreferencesUtil preferencesUtil = locator<SharedPreferencesUtil>();
    String _ipAddress = preferencesUtil.getIpAddress();
    int _port = preferencesUtil.getPortNumber();
    String _publishName = preferencesUtil.getPublishName();
    print("IpAddress: $_ipAddress");
    return "http://$_ipAddress:$_port/$_publishName/content/EmployeePic/";
  }

  String buildRelativeFilePath(){
    SharedPreferencesUtil preferencesUtil = locator<SharedPreferencesUtil>();
    String _ipAddress = preferencesUtil.getIpAddress();
    int _port = preferencesUtil.getPortNumber();
    String _publishName = preferencesUtil.getPublishName();
    print("IpAddress: $_ipAddress");
    return "http://$_ipAddress:$_port/$_publishName/Content/ProgressAttachments/";
  }

  Future<dynamic> get(String url, {bool isRelative = true}) async {
    var responseJson;
    try {
      String fullUrl = isRelative
          ? (buildUrl() + url)
          : url; // todo uncomment this line when the server is setup;
      print("Full Url : " + fullUrl);
      final response =
          await http.get(Uri.parse(fullUrl)).timeout(Duration(seconds: 12));
      responseJson = _returnResponse(response);
    } on TimeoutException catch (e) {
      print("Timeout exception: ${e.toString()}");
      throw TimeOutException("Server took to long to respond.");
    } on SocketException catch (e) {
      print("SocketException : ${e.toString()}");
      throw FetchDataException('No Internet connection');
    } on FormatException catch (e) {
      print("FormatException: ${e.toString()}");
      throw InvalidFormatException("Bad response format");
    }
    return responseJson;
  }

  Future<String?> uploadMultipleFiles(
      List<File?>? docFiles, File? financeFile, String url) async {
    var request =
        new http.MultipartRequest("POST", Uri.parse(buildUrl() + url));

    if(docFiles != null){
      for (var file in docFiles) {
        if (file != null) {
          String fileName = file.path.split("/").last;
          var stream = file.readAsBytes().asStream();
          var length = await file.length();
          print("File length - $length");
          print("fileName - $fileName");
          // multipart that takes file

          var multipartFileSign =
          new http.MultipartFile('file', stream, length, filename: fileName);
          request.files.add(multipartFileSign);
        }
      }
    }
    if (financeFile != null) {
      String fileName = financeFile.path.split("/").last;
      var stream = financeFile.readAsBytes().asStream();
      var length = await financeFile.length();
      var financeMultipartFileSign = new http.MultipartFile(
          "finance", stream, length,
          filename: fileName);
      request.files.add(financeMultipartFileSign);
    }
    // send
    var response = await request.send();

    print(response.statusCode);

    // listen for response
    response.stream.transform(utf8.decoder).listen((value) {
      print(value);
    });

    return response.reasonPhrase;
  }

  Future<String> uploadConversation(String relativePath) async{
    var request = http.MultipartRequest('POST', Uri.parse(buildUrl() + relativePath));
    print("Conversation post url: ${buildUrl() + relativePath}");
    var res = await request.send();
    print("Uploaded");
    return res.stream.bytesToString();

  }
  Future<String> uploadApproval(String relativePath) async{
    var request = http.MultipartRequest('POST', Uri.parse(buildUrl() + relativePath));
    print("Approval request post url: ${buildUrl() + relativePath}");
    var res = await request.send();
    print("Uploaded");
    return res.stream.bytesToString();
  }

  dynamic _returnResponse(http.Response response) {
    print("Response status code : " + response.statusCode.toString());
    print("Response body : " + response.body.toString());
    switch (response.statusCode) {
      case 200:
        var responseJson = json.decode(response.body.toString());
        return responseJson;
      case 400:
        throw BadRequestException(response.body.toString());
      case 401:
      case 403:
        throw UnauthorisedException(response.body.toString());
      case 404:
        throw NotFoundException(response.body.toString());
      case 500:
      default:
        throw FetchDataException(
            'Error occurred while communicating with a server');
    }
  }
}
