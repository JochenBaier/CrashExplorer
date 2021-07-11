/*
   This file is part of CrashExplorer.
   
   CrashExplorer is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.
   
   CrashExplorer is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
   GNU General Public License for more details.
   
   You should have received a copy of the GNU General Public License
   along with CrashExplorer.  If not, see <http://www.gnu.org/licenses/>.
*/

namespace CrashExplorer.library
{
  /// <summary>
  /// From winnt.h. Copyright (c) Microsoft Corporation. All rights reserved.
  /// </summary>
  ///
  public static class ExceptionCodes
  {
    private const uint STATUS_ABANDONED_WAIT_0          =(uint)0x00000080L;
    private const uint STATUS_USER_APC                  =(uint)0x000000C0L;
    private const uint STATUS_TIMEOUT                   =(uint)0x00000102L;
    private const uint STATUS_PENDING                   =(uint)0x00000103L;
    private const uint DBG_EXCEPTION_HANDLED            =(uint)0x00010001L;
    private const uint DBG_CONTINUE                     =(uint)0x00010002L;
    private const uint STATUS_SEGMENT_NOTIFICATION      =(uint)0x40000005L;
    private const uint STATUS_FATAL_APP_EXIT            =(uint)0x40000015L;
    private const uint DBG_TERMINATE_THREAD             =(uint)0x40010003L;
    private const uint DBG_TERMINATE_PROCESS            =(uint)0x40010004L;
    private const uint DBG_CONTROL_C                    =(uint)0x40010005L;
    private const uint DBG_PRINTEXCEPTION_C             =(uint)0x40010006L;
    private const uint DBG_RIPEXCEPTION                 =(uint)0x40010007L;
    private const uint DBG_CONTROL_BREAK                =(uint)0x40010008L;
    private const uint DBG_COMMAND_EXCEPTION            =(uint)0x40010009L;
    private const uint STATUS_GUARD_PAGE_VIOLATION      =(uint)0x80000001L;
    private const uint STATUS_DATATYPE_MISALIGNMENT     =(uint)0x80000002L;
    private const uint STATUS_BREAKPOINT                =(uint)0x80000003L;
    private const uint STATUS_SINGLE_STEP               =(uint)0x80000004L;
    private const uint STATUS_LONGJUMP                  =(uint)0x80000026L;
    private const uint STATUS_UNWIND_CONSOLIDATE        =(uint)0x80000029L;
    private const uint DBG_EXCEPTION_NOT_HANDLED        =(uint)0x80010001L;
    private const uint STATUS_ACCESS_VIOLATION          =(uint)0xC0000005L;
    private const uint STATUS_IN_PAGE_ERROR             =(uint)0xC0000006L;
    private const uint STATUS_INVALID_HANDLE            =(uint)0xC0000008L;
    private const uint STATUS_INVALID_PARAMETER         =(uint)0xC000000DL;
    private const uint STATUS_NO_MEMORY                 =(uint)0xC0000017L;
    private const uint STATUS_ILLEGAL_INSTRUCTION       =(uint)0xC000001DL;
    private const uint STATUS_NONCONTINUABLE_EXCEPTION  =(uint)0xC0000025L;
    private const uint STATUS_INVALID_DISPOSITION       =(uint)0xC0000026L;
    private const uint STATUS_ARRAY_BOUNDS_EXCEEDED     =(uint)0xC000008CL;
    private const uint STATUS_FLOAT_DENORMAL_OPERAND    =(uint)0xC000008DL;
    private const uint STATUS_FLOAT_DIVIDE_BY_ZERO      =(uint)0xC000008EL;
    private const uint STATUS_FLOAT_INEXACT_RESULT      =(uint)0xC000008FL;
    private const uint STATUS_FLOAT_INVALID_OPERATION   =(uint)0xC0000090L;
    private const uint STATUS_FLOAT_OVERFLOW            =(uint)0xC0000091L;
    private const uint STATUS_FLOAT_STACK_CHECK         =(uint)0xC0000092L;
    private const uint STATUS_FLOAT_UNDERFLOW           =(uint)0xC0000093L;
    private const uint STATUS_INTEGER_DIVIDE_BY_ZERO    =(uint)0xC0000094L;
    private const uint STATUS_INTEGER_OVERFLOW          =(uint)0xC0000095L;
    private const uint STATUS_PRIVILEGED_INSTRUCTION    =(uint)0xC0000096L;
    private const uint STATUS_STACK_OVERFLOW            =(uint)0xC00000FDL;
    private const uint STATUS_DLL_NOT_FOUND             =(uint)0xC0000135L;
    private const uint STATUS_ORDINAL_NOT_FOUND         =(uint)0xC0000138L;
    private const uint STATUS_ENTRYPOINT_NOT_FOUND      =(uint)0xC0000139L;
    private const uint STATUS_CONTROL_C_EXIT            =(uint)0xC000013AL;
    private const uint STATUS_DLL_INIT_FAILED           =(uint)0xC0000142L;
    private const uint STATUS_CONTROL_STACK_VIOLATION   =(uint)0xC00001B2L;
    private const uint STATUS_FLOAT_MULTIPLE_FAULTS     =(uint)0xC00002B4L;
    private const uint STATUS_FLOAT_MULTIPLE_TRAPS      =(uint)0xC00002B5L;
    private const uint STATUS_REG_NAT_CONSUMPTION       =(uint)0xC00002C9L;
    private const uint STATUS_HEAP_CORRUPTION           =(uint)0xC0000374L;
    private const uint STATUS_STACK_BUFFER_OVERRUN      =(uint)0xC0000409L;
    private const uint STATUS_INVALID_CRUNTIME_PARAMETER=(uint)0xC0000417L;
    private const uint STATUS_ASSERTION_FAILURE         =(uint)0xC0000420L;
    private const uint STATUS_SXS_EARLY_DEACTIVATION    =(uint)0xC015000FL;
    private const uint STATUS_SXS_INVALID_DEACTIVATION  =(uint)0xC0150010L;
    public static string ToString(uint exceptionCode)
    {
      switch (exceptionCode)
      {
        case 1: return "TERMINATED";
        case 3: return "ABORT";
        case STATUS_ABANDONED_WAIT_0: return "ABANDONED_WAIT_0";
        case STATUS_USER_APC: return "USER_APC";
        case STATUS_TIMEOUT: return "TIMEOUT";
        case STATUS_PENDING: return "PENDING";
        case DBG_EXCEPTION_HANDLED: return "DBG_EXCEPTION_HANDLED";
        case DBG_CONTINUE: return "DBG_CONTINUE";
        case STATUS_SEGMENT_NOTIFICATION: return "SEGMENT_NOTIFICATION";
        case STATUS_FATAL_APP_EXIT: return "FATAL_APP_EXIT";
        case DBG_TERMINATE_THREAD: return "DBG_TERMINATE_THREAD";
        case DBG_TERMINATE_PROCESS: return "DBG_TERMINATE_PROCESS";
        case DBG_CONTROL_C: return "DBG_CONTROL_C";
        case DBG_PRINTEXCEPTION_C: return "DBG_PRINTEXCEPTION_C";
        case DBG_RIPEXCEPTION: return "DBG_RIPEXCEPTION";
        case DBG_CONTROL_BREAK: return "DBG_CONTROL_BREAK";
        case DBG_COMMAND_EXCEPTION: return "DBG_COMMAND_EXCEPTION";
        case STATUS_GUARD_PAGE_VIOLATION: return "GUARD_PAGE_VIOLATION";
        case STATUS_DATATYPE_MISALIGNMENT: return "DATATYPE_MISALIGNMENT";
        case STATUS_BREAKPOINT: return "BREAKPOINT";
        case STATUS_SINGLE_STEP: return "SINGLE_STEP";
        case STATUS_LONGJUMP: return "LONGJUMP";
        case STATUS_UNWIND_CONSOLIDATE: return "UNWIND_CONSOLIDATE";
        case DBG_EXCEPTION_NOT_HANDLED: return "DBG_EXCEPTION_NOT_HANDLED";
        case STATUS_ACCESS_VIOLATION: return "ACCESS_VIOLATION";
        case STATUS_IN_PAGE_ERROR: return "IN_PAGE_ERROR";
        case STATUS_INVALID_HANDLE: return "INVALID_HANDLE";
        case STATUS_INVALID_PARAMETER: return "INVALID_PARAMETER";
        case STATUS_NO_MEMORY: return "NO_MEMORY";
        case STATUS_ILLEGAL_INSTRUCTION: return "ILLEGAL_INSTRUCTION";
        case STATUS_NONCONTINUABLE_EXCEPTION: return "NONCONTINUABLE_EXCEPTION";
        case STATUS_INVALID_DISPOSITION: return "INVALID_DISPOSITION";
        case STATUS_ARRAY_BOUNDS_EXCEEDED: return "ARRAY_BOUNDS_EXCEEDED";
        case STATUS_FLOAT_DENORMAL_OPERAND: return "FLOAT_DENORMAL_OPERAND";
        case STATUS_FLOAT_DIVIDE_BY_ZERO: return "FLOAT_DIVIDE_BY_ZERO";
        case STATUS_FLOAT_INEXACT_RESULT: return "FLOAT_INEXACT_RESULT";
        case STATUS_FLOAT_INVALID_OPERATION: return "FLOAT_INVALID_OPERATION";
        case STATUS_FLOAT_OVERFLOW: return "FLOAT_OVERFLOW";
        case STATUS_FLOAT_STACK_CHECK: return "FLOAT_STACK_CHECK";
        case STATUS_FLOAT_UNDERFLOW: return "FLOAT_UNDERFLOW";
        case STATUS_INTEGER_DIVIDE_BY_ZERO: return "INTEGER_DIVIDE_BY_ZERO";
        case STATUS_INTEGER_OVERFLOW: return "INTEGER_OVERFLOW";
        case STATUS_PRIVILEGED_INSTRUCTION: return "PRIVILEGED_INSTRUCTION";
        case STATUS_STACK_OVERFLOW: return "STACK_OVERFLOW";
        case STATUS_DLL_NOT_FOUND: return "DLL_NOT_FOUND";
        case STATUS_ORDINAL_NOT_FOUND: return "ORDINAL_NOT_FOUND";
        case STATUS_ENTRYPOINT_NOT_FOUND: return "ENTRYPOINT_NOT_FOUND";
        case STATUS_CONTROL_C_EXIT: return "CONTROL_C_EXIT";
        case STATUS_DLL_INIT_FAILED: return "DLL_INIT_FAILED";
        case STATUS_CONTROL_STACK_VIOLATION: return "CONTROL_STACK_VIOLATION";
        case STATUS_FLOAT_MULTIPLE_FAULTS: return "FLOAT_MULTIPLE_FAULTS";
        case STATUS_FLOAT_MULTIPLE_TRAPS: return "FLOAT_MULTIPLE_TRAPS";
        case STATUS_REG_NAT_CONSUMPTION: return "REG_NAT_CONSUMPTION";
        case STATUS_HEAP_CORRUPTION: return "HEAP_CORRUPTION";
        case STATUS_STACK_BUFFER_OVERRUN: return "STACK_BUFFER_OVERRUN";
        case STATUS_INVALID_CRUNTIME_PARAMETER: return "INVALID_CRUNTIME_PARAMETER";
        case STATUS_ASSERTION_FAILURE: return "ASSERTION_FAILURE";
        case STATUS_SXS_EARLY_DEACTIVATION: return "SXS_EARLY_DEACTIVATION";
        case STATUS_SXS_INVALID_DEACTIVATION: return "SXS_INVALID_DEACTIVATION";

        default:
        {
          return $"UNKNOWN ('0x{exceptionCode:x}')";
        }
      }

    }
  }
}
