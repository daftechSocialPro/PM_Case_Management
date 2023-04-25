class AppException implements Exception {
  final _message;
  final _prefix;

  AppException([this._message, this._prefix]);

  String toString() {
    return "$_prefix$_message";
  }
}

class FetchDataException extends AppException {
  FetchDataException([message])
      : super(message, "Error during communication: ");
}

class BadRequestException extends AppException {
  BadRequestException([message]) : super(message, "Invalid Request: ");
}

class UnauthorisedException extends AppException {
  UnauthorisedException([message]) : super(message, "Unauthorised: ");
}

class InvalidInputException extends AppException {
  InvalidInputException(message) : super(message, "Invalid Input: ");
}
class InvalidFormatException extends AppException {
  InvalidFormatException(message) : super(message, "Invalid Format: ");
}

class NotFoundException extends AppException {
  NotFoundException(message) : super(message,"");
}

class TimeOutException extends AppException {
  TimeOutException(message) : super(message,"IpAddress can't be reached: ");
}