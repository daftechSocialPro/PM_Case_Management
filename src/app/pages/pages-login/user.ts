export interface User {

    userName: string ;
    password: string ;
}
export interface UserView {
    FullName : string ; 
    role: string [];
    UserID : string ;
    EmployeeId:string;
}

export interface Token {
    token :string ;
}