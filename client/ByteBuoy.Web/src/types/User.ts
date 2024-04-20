import { UserRole } from "./UserRole";

export interface User {
    id: string;
    email: string;
    roles: UserRole[];
}