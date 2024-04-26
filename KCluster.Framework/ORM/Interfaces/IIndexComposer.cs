﻿namespace KCluster.Framework.ORM.Interfaces;

public interface IIndexComposer<out T>
{
    T Compose(IIndex index);
}