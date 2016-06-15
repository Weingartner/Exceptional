
using System;
using System.Collections.Generic;
using Weingartner.Exceptional;

namespace Weingartner.Exceptional {
    public  static partial class Exceptional {

    
    /// <Summary>
    /// If any of the inputs are in error then the output will contain an aggregate
    /// exception of the inputs. If the selector function throws then the output will
    /// contain that exception.
    /// </Summary>
    public static IExceptional<TU> Combine<T1, TU>
        ( IExceptional<T1> t1
        , Func<T1, TU> fn)
    {
        var errors = new List<Exception>();
                if(t1.HasException)
            errors.Add(t1.Exception);
        
		if (errors.Count==1)
		{
			return  new Exceptional<TU>(errors[0]);
		}
        else if (errors.Count>1)
		{
			var ex = new AggregateException(errors);
			ex.Flatten();
			return  new Exceptional<TU>(ex);
		}

        try{
            return fn(t1.Value).ToExceptional();
        }catch(Exception e){
            return new Exceptional<TU>(e);
        }
    }

    
    /// <Summary>
    /// If any of the inputs are in error then the output will contain an aggregate
    /// exception of the inputs. If the selector function throws then the output will
    /// contain that exception.
    /// </Summary>
    public static IExceptional<TU> Combine<T1, T2, TU>
        ( IExceptional<T1> t1, IExceptional<T2> t2
        , Func<T1, T2, TU> fn)
    {
        var errors = new List<Exception>();
                if(t1.HasException)
            errors.Add(t1.Exception);
                if(t2.HasException)
            errors.Add(t2.Exception);
        
		if (errors.Count==1)
		{
			return  new Exceptional<TU>(errors[0]);
		}
        else if (errors.Count>1)
		{
			var ex = new AggregateException(errors);
			ex.Flatten();
			return  new Exceptional<TU>(ex);
		}

        try{
            return fn(t1.Value, t2.Value).ToExceptional();
        }catch(Exception e){
            return new Exceptional<TU>(e);
        }
    }

    
    /// <Summary>
    /// If any of the inputs are in error then the output will contain an aggregate
    /// exception of the inputs. If the selector function throws then the output will
    /// contain that exception.
    /// </Summary>
    public static IExceptional<TU> Combine<T1, T2, T3, TU>
        ( IExceptional<T1> t1, IExceptional<T2> t2, IExceptional<T3> t3
        , Func<T1, T2, T3, TU> fn)
    {
        var errors = new List<Exception>();
                if(t1.HasException)
            errors.Add(t1.Exception);
                if(t2.HasException)
            errors.Add(t2.Exception);
                if(t3.HasException)
            errors.Add(t3.Exception);
        
		if (errors.Count==1)
		{
			return  new Exceptional<TU>(errors[0]);
		}
        else if (errors.Count>1)
		{
			var ex = new AggregateException(errors);
			ex.Flatten();
			return  new Exceptional<TU>(ex);
		}

        try{
            return fn(t1.Value, t2.Value, t3.Value).ToExceptional();
        }catch(Exception e){
            return new Exceptional<TU>(e);
        }
    }

    
    /// <Summary>
    /// If any of the inputs are in error then the output will contain an aggregate
    /// exception of the inputs. If the selector function throws then the output will
    /// contain that exception.
    /// </Summary>
    public static IExceptional<TU> Combine<T1, T2, T3, T4, TU>
        ( IExceptional<T1> t1, IExceptional<T2> t2, IExceptional<T3> t3, IExceptional<T4> t4
        , Func<T1, T2, T3, T4, TU> fn)
    {
        var errors = new List<Exception>();
                if(t1.HasException)
            errors.Add(t1.Exception);
                if(t2.HasException)
            errors.Add(t2.Exception);
                if(t3.HasException)
            errors.Add(t3.Exception);
                if(t4.HasException)
            errors.Add(t4.Exception);
        
		if (errors.Count==1)
		{
			return  new Exceptional<TU>(errors[0]);
		}
        else if (errors.Count>1)
		{
			var ex = new AggregateException(errors);
			ex.Flatten();
			return  new Exceptional<TU>(ex);
		}

        try{
            return fn(t1.Value, t2.Value, t3.Value, t4.Value).ToExceptional();
        }catch(Exception e){
            return new Exceptional<TU>(e);
        }
    }

    
    /// <Summary>
    /// If any of the inputs are in error then the output will contain an aggregate
    /// exception of the inputs. If the selector function throws then the output will
    /// contain that exception.
    /// </Summary>
    public static IExceptional<TU> Combine<T1, T2, T3, T4, T5, TU>
        ( IExceptional<T1> t1, IExceptional<T2> t2, IExceptional<T3> t3, IExceptional<T4> t4, IExceptional<T5> t5
        , Func<T1, T2, T3, T4, T5, TU> fn)
    {
        var errors = new List<Exception>();
                if(t1.HasException)
            errors.Add(t1.Exception);
                if(t2.HasException)
            errors.Add(t2.Exception);
                if(t3.HasException)
            errors.Add(t3.Exception);
                if(t4.HasException)
            errors.Add(t4.Exception);
                if(t5.HasException)
            errors.Add(t5.Exception);
        
		if (errors.Count==1)
		{
			return  new Exceptional<TU>(errors[0]);
		}
        else if (errors.Count>1)
		{
			var ex = new AggregateException(errors);
			ex.Flatten();
			return  new Exceptional<TU>(ex);
		}

        try{
            return fn(t1.Value, t2.Value, t3.Value, t4.Value, t5.Value).ToExceptional();
        }catch(Exception e){
            return new Exceptional<TU>(e);
        }
    }

    
    /// <Summary>
    /// If any of the inputs are in error then the output will contain an aggregate
    /// exception of the inputs. If the selector function throws then the output will
    /// contain that exception.
    /// </Summary>
    public static IExceptional<TU> Combine<T1, T2, T3, T4, T5, T6, TU>
        ( IExceptional<T1> t1, IExceptional<T2> t2, IExceptional<T3> t3, IExceptional<T4> t4, IExceptional<T5> t5, IExceptional<T6> t6
        , Func<T1, T2, T3, T4, T5, T6, TU> fn)
    {
        var errors = new List<Exception>();
                if(t1.HasException)
            errors.Add(t1.Exception);
                if(t2.HasException)
            errors.Add(t2.Exception);
                if(t3.HasException)
            errors.Add(t3.Exception);
                if(t4.HasException)
            errors.Add(t4.Exception);
                if(t5.HasException)
            errors.Add(t5.Exception);
                if(t6.HasException)
            errors.Add(t6.Exception);
        
		if (errors.Count==1)
		{
			return  new Exceptional<TU>(errors[0]);
		}
        else if (errors.Count>1)
		{
			var ex = new AggregateException(errors);
			ex.Flatten();
			return  new Exceptional<TU>(ex);
		}

        try{
            return fn(t1.Value, t2.Value, t3.Value, t4.Value, t5.Value, t6.Value).ToExceptional();
        }catch(Exception e){
            return new Exceptional<TU>(e);
        }
    }

    
    /// <Summary>
    /// If any of the inputs are in error then the output will contain an aggregate
    /// exception of the inputs. If the selector function throws then the output will
    /// contain that exception.
    /// </Summary>
    public static IExceptional<TU> Combine<T1, T2, T3, T4, T5, T6, T7, TU>
        ( IExceptional<T1> t1, IExceptional<T2> t2, IExceptional<T3> t3, IExceptional<T4> t4, IExceptional<T5> t5, IExceptional<T6> t6, IExceptional<T7> t7
        , Func<T1, T2, T3, T4, T5, T6, T7, TU> fn)
    {
        var errors = new List<Exception>();
                if(t1.HasException)
            errors.Add(t1.Exception);
                if(t2.HasException)
            errors.Add(t2.Exception);
                if(t3.HasException)
            errors.Add(t3.Exception);
                if(t4.HasException)
            errors.Add(t4.Exception);
                if(t5.HasException)
            errors.Add(t5.Exception);
                if(t6.HasException)
            errors.Add(t6.Exception);
                if(t7.HasException)
            errors.Add(t7.Exception);
        
		if (errors.Count==1)
		{
			return  new Exceptional<TU>(errors[0]);
		}
        else if (errors.Count>1)
		{
			var ex = new AggregateException(errors);
			ex.Flatten();
			return  new Exceptional<TU>(ex);
		}

        try{
            return fn(t1.Value, t2.Value, t3.Value, t4.Value, t5.Value, t6.Value, t7.Value).ToExceptional();
        }catch(Exception e){
            return new Exceptional<TU>(e);
        }
    }

    
    /// <Summary>
    /// If any of the inputs are in error then the output will contain an aggregate
    /// exception of the inputs. If the selector function throws then the output will
    /// contain that exception.
    /// </Summary>
    public static IExceptional<TU> Combine<T1, T2, T3, T4, T5, T6, T7, T8, TU>
        ( IExceptional<T1> t1, IExceptional<T2> t2, IExceptional<T3> t3, IExceptional<T4> t4, IExceptional<T5> t5, IExceptional<T6> t6, IExceptional<T7> t7, IExceptional<T8> t8
        , Func<T1, T2, T3, T4, T5, T6, T7, T8, TU> fn)
    {
        var errors = new List<Exception>();
                if(t1.HasException)
            errors.Add(t1.Exception);
                if(t2.HasException)
            errors.Add(t2.Exception);
                if(t3.HasException)
            errors.Add(t3.Exception);
                if(t4.HasException)
            errors.Add(t4.Exception);
                if(t5.HasException)
            errors.Add(t5.Exception);
                if(t6.HasException)
            errors.Add(t6.Exception);
                if(t7.HasException)
            errors.Add(t7.Exception);
                if(t8.HasException)
            errors.Add(t8.Exception);
        
		if (errors.Count==1)
		{
			return  new Exceptional<TU>(errors[0]);
		}
        else if (errors.Count>1)
		{
			var ex = new AggregateException(errors);
			ex.Flatten();
			return  new Exceptional<TU>(ex);
		}

        try{
            return fn(t1.Value, t2.Value, t3.Value, t4.Value, t5.Value, t6.Value, t7.Value, t8.Value).ToExceptional();
        }catch(Exception e){
            return new Exceptional<TU>(e);
        }
    }

    

    }
}