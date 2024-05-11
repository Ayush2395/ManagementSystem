import React from "react";
import moment from "moment";
import { UserClient, UserDto } from "../web-api-client.ts";
import { Link, useNavigate } from "react-router-dom";
import { Pencil, Trash2 } from "lucide-react";

type TableProps = {
    list: Record<string, string>
    users: UserDto[]
}

const Table: React.FC<TableProps> = ({ list, users }) => {
    const navigate = useNavigate()

    const handleDeleteUser = async (uid: string) => {
        const client = new UserClient()
        try {
            const response = await client.deleteUser(uid)
            if (response.succeeded) {
                navigate("/users")
                console.log("user deleted");
            } else {
                console.log(response.errors);
            }
        } catch (error) {
            if (error instanceof TypeError) {
                console.log(error.message);
            } else if (error instanceof ReferenceError) {
                console.log(error.message);
            } else {
                console.log(error);
            }
        }
    }

    return <>
        <table className="table table-responsive">
            <thead>
                <tr>

                    {
                        users.length > 0 && Object.entries(users[0]).map(([key, value], idx) => {
                            const columnName = list[key]
                            return <th key={idx}>{columnName}</th>
                        })
                    }
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                {
                    users.length > 0 ? Object.values(users).map((value, idx) => <tr key={idx * 3.5}>
                        <td><Link to={`/${value.id}`} className="nav-link">User {idx + 1}</Link></td>
                        <td>{value.firstName}</td>
                        <td>{value.lastName}</td>
                        <td>{moment(value.created).format("lll")}</td>
                        <td>{value.lastModified ? moment(value.lastModified).format("lll") : "N/A"}</td>
                        <td key={idx * 36.5}>
                            <div className="btn-group">
                                <Link to={`/edit/${value.id}`} type="button" className="btn btn-sm btn-warning"><Pencil strokeWidth={1.25} size={20} /></Link>
                                <button onClick={() => handleDeleteUser(value.id ?? "")} type="button" className="btn btn-sm btn-danger"><Trash2 strokeWidth={1.25} size={20} /></button>
                            </div>
                        </td>
                    </tr>)
                        : <tr>
                            <td className="text-center">Loading...</td>
                        </tr>
                }
            </tbody>
        </table>
    </>
}
export default Table